using System.Threading;
using CommunityToolkit.HighPerformance.Buffers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Polly.Retry;

namespace MyTelegram.EventBus.RabbitMQ;

public sealed class RabbitMqEventBus(
    ILogger<RabbitMqEventBus> logger,
    IServiceProvider serviceProvider,
    IOptions<EventBusOptions> options,
    IOptionsMonitor<RabbitMqOptions> rabbitmqOptions,
    IRabbitMqSerializer rabbitMqSerializer,
    IOptions<EventBusSubscriptionInfo> subscriptionOptions) : IEventBus,
    IDisposable, IHostedService
{
    private const string ExchangeName = "mytelegram_exchange";

    private readonly ResiliencePipeline _pipeline = CreateResiliencePipeline(options.Value.RetryCount);
    private readonly string _queueName = options.Value.ClientName;
    private readonly EventBusSubscriptionInfo _subscriptionInfo = subscriptionOptions.Value;
    private IConnection? _rabbitMqConnection;

    private IChannel? _consumerChannel;

    public async Task PublishAsync<TEventData>(TEventData eventData, string? eventType = null)
        where TEventData : class
    {
        var routingKey = eventType ?? eventData.GetType().Name;

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("Creating RabbitMQ channel to publish event: ({EventName})", routingKey);
        }

        await using var channel = await _rabbitMqConnection?.CreateChannelAsync()! ?? throw new InvalidOperationException("RabbitMQ connection is not open");

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventName}", routingKey);
        }

        await channel.ExchangeDeclareAsync(exchange: ExchangeName, type: "direct");

        var writer = new ArrayPoolBufferWriter<byte>();
        rabbitMqSerializer.Serialize(writer, eventData);
        var body = writer.WrittenMemory;
        
        await _pipeline.Execute(async () =>
        {

            // Depending on Sampling (and whether a listener is registered or not), the activity above may not be created.
            // If it is created, then propagate its context. If it is not created, the propagate the Current context, if any.

            //var properties = channel.CreateBasicProperties();
            var properties = new BasicProperties
            {
                // persistent
                DeliveryMode = DeliveryModes.Persistent
            };

            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogTrace("Publishing event to RabbitMQ: {EventName}", routingKey);
            }

            try
            {
                await channel.BasicPublishAsync(
                    exchange: ExchangeName,
                    routingKey: routingKey,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
            }
            finally
            {
                writer.Dispose();
            }
        });
    }

    public void Dispose()
    {
        _consumerChannel?.Dispose();
    }

    private async Task OnMessageReceived(object sender, BasicDeliverEventArgs eventArgs)
    {
        var eventName = eventArgs.RoutingKey;
        try
        {
            await ProcessEvent(eventName, eventArgs.Body);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Error Processing message \"{Message}\"", eventName);
        }

        // Even on exception we take the message off the queue.
        // in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX). 
        // For more information see: https://www.rabbitmq.com/dlx.html
        if (_consumerChannel != null)
        {
            await _consumerChannel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
        }
        else
        {
            logger.LogWarning("Consumer channel is null");
        }
    }

    private async Task ProcessEvent(string eventName, ReadOnlyMemory<byte> body)
    {
        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);
        }

        if (!_subscriptionInfo.EventTypes.TryGetValue(eventName, out var eventType))
        {
            logger.LogWarning("Unable to resolve event type for event name {EventName}", eventName);
            return;
        }

        //if (_subscriptionInfo.RawDataHandledEventTypes.Contains(eventName))
        //{
        //    var rawEventData = new EventBusRawDataReceivedEvent(eventType, body, false);
        //    foreach (var handler in serviceProvider.GetKeyedServices<IEventHandler<EventBusRawDataReceivedEvent>>(typeof(EventBusRawDataReceivedEvent)))
        //    {
        //        await handler.HandleEventAsync(rawEventData);
        //    }

        //    //if (rawEventData.Handled)
        //    {
        //        return;
        //    }
        //}

        var eventData = rabbitMqSerializer.Deserialize(eventType, body);
        if (_subscriptionInfo.TryGetHandlers(eventType, out var handlers))
        {
            foreach (var handler in handlers ?? [])
            {
                await handler(eventData, serviceProvider);
            }
        }

        // Get all the handlers using the event type as the key
        //foreach (var handler in scope.ServiceProvider.GetKeyedServices<IEventHandler>(eventType))
        //{
        //    await handler.HandleEventAsync(eventData);
        //}
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Messaging is async so we don't need to wait for it to complete. On top of this
        // the APIs are blocking, so we need to run this on a background thread.
        _ = Task.Factory.StartNew(async () =>
            {
            LabelStart:
                try
                {
                    logger.LogInformation("Starting RabbitMQ connection on a background thread");
                    //var connectionFactory = serviceProvider.GetRequiredService<IConnectionFactory>();
                    var connectionFactory = new ConnectionFactory
                    {
                        HostName = rabbitmqOptions.CurrentValue.HostName,
                        Port = rabbitmqOptions.CurrentValue.Port,
                        UserName = rabbitmqOptions.CurrentValue.UserName,
                        Password = rabbitmqOptions.CurrentValue.Password,
                        AutomaticRecoveryEnabled = true
                    };
                    _rabbitMqConnection = await connectionFactory.CreateConnectionAsync(cancellationToken);// serviceProvider.GetRequiredService<IConnection>();
                    if (!_rabbitMqConnection.IsOpen)
                    {
                        return;
                    }

                    if (logger.IsEnabled(LogLevel.Trace))
                    {
                        logger.LogTrace("Creating RabbitMQ consumer channel");
                    }

                    _consumerChannel = await _rabbitMqConnection.CreateChannelAsync(cancellationToken: cancellationToken);
                    _consumerChannel.CallbackExceptionAsync += (_, ea) =>
                    {
                        logger.LogWarning(ea.Exception, "Error with RabbitMQ consumer channel");
                        return Task.CompletedTask;
                    };

                    await _consumerChannel.ExchangeDeclareAsync(exchange: ExchangeName,
                        type: "direct", cancellationToken: cancellationToken);

                    await _consumerChannel.QueueDeclareAsync(queue: _queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null, cancellationToken: cancellationToken);

                    if (logger.IsEnabled(LogLevel.Trace))
                    {
                        logger.LogTrace("Starting RabbitMQ basic consume");
                    }

                    var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                    consumer.ReceivedAsync += OnMessageReceived;

                    await _consumerChannel.BasicConsumeAsync(
                        queue: _queueName,
                        autoAck: false,
                        consumer: consumer, cancellationToken: cancellationToken);

                    foreach (var (eventName, _) in _subscriptionInfo.EventTypes)
                    {
                        await _consumerChannel.QueueBindAsync(
                            queue: _queueName,
                            exchange: ExchangeName,
                            routingKey: eventName, cancellationToken: cancellationToken);
                    }

                    logger.LogInformation("RabbitMQ connection created");

                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error starting RabbitMQ connection");
                    await Task.Delay(2000, cancellationToken);
                    goto LabelStart;
                }
            },
            TaskCreationOptions.LongRunning);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private static ResiliencePipeline CreateResiliencePipeline(int retryCount)
    {
        // See https://www.pollydocs.org/strategies/retry.html
        var retryOptions = new RetryStrategyOptions
        {
            ShouldHandle = new PredicateBuilder().Handle<BrokerUnreachableException>().Handle<SocketException>(),
            MaxRetryAttempts = retryCount,
            DelayGenerator = (context) => ValueTask.FromResult(GenerateDelay(context.AttemptNumber))
        };

        return new ResiliencePipelineBuilder()
            .AddRetry(retryOptions)
            .Build();

        static TimeSpan? GenerateDelay(int attempt)
        {
            return TimeSpan.FromSeconds(Math.Pow(2, attempt));
        }
    }
}