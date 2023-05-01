using Microsoft.Extensions.Options;

namespace MyTelegram.EventBus.RabbitMQ;

public class EventBusRabbitMq : IEventBus, IDisposable
{
    private readonly IEventHandlerInvoker _eventHandlerInvoker;
    private readonly ILogger<EventBusRabbitMq> _logger;
    private readonly IOptions<EventBusRabbitMqOptions> _options;
    private readonly IRabbitMqPersistentConnection _persistentConnection;
    private readonly IRabbitMqSerializer _rabbitMqSerializer;
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventBusSubscriptionsManager _subsManager;
    private IModel? _consumerChannel;

    public EventBusRabbitMq(IRabbitMqPersistentConnection persistentConnection,
        ILogger<EventBusRabbitMq> logger,
        IEventBusSubscriptionsManager? subsManager,
        IServiceProvider serviceProvider,
        IEventHandlerInvoker eventHandlerInvoker,
        IOptions<EventBusRabbitMqOptions> options,
        IRabbitMqSerializer rabbitMqSerializer)
    {
        _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _serviceProvider = serviceProvider;
        _eventHandlerInvoker = eventHandlerInvoker;
        _options = options;
        _rabbitMqSerializer = rabbitMqSerializer;
        _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
        _consumerChannel = CreateConsumerChannel();
        _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
    }

    public void Dispose()
    {
        if (_consumerChannel != null)
        {
            _consumerChannel.Dispose();
        }

        _subsManager.Clear();
    }

    public Task PublishAsync<TEventData>(TEventData eventData) where TEventData : class
    {
        return PublishAsync(eventData.GetType().UnderlyingSystemType, SerializeToBytes(eventData));
    }

    public Task PublishAsync(Type eventDataType,
        object eventData)
    {
        var eventDataBytes = _rabbitMqSerializer.Serialize(eventData);
        return PublishAsync(eventDataType, eventDataBytes);

        //if (!_persistentConnection.IsConnected)
        //{
        //    _persistentConnection.TryConnect();
        //}

        //var eventName = _subsManager.GetEventKey(eventDataType);

        //var policy = Policy.Handle<BrokerUnreachableException>()
        //    .Or<SocketException>()
        //    .WaitAndRetry(_options.Value.RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
        //    {
        //        _logger.LogWarning(ex, "Could not publish event: {EventType} after {Timeout}s ({ExceptionMessage})", eventData.GetType().Name, $"{time.TotalSeconds:n1}", ex.Message);
        //    });

        //_logger.LogTrace("Creating RabbitMQ channel to publish event:({EventName}) {EventType}", eventName, eventDataType);

        //using var channel = _persistentConnection.CreateModel();
        //_logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventName} {EventType}", eventName, eventDataType);

        //channel.ExchangeDeclare(exchange: _options.Value.ExchangeName, durable: true, type: "direct");
        //try
        //{
        //    //var body = JsonSerializer.SerializeToUtf8Bytes(eventData, eventData.GetType(), _jsonContextProvider.GetJsonSerializerContext());
        //    var body = _rabbitMqSerializer.Serialize(eventData);

        //    policy.Execute(() =>
        //    {
        //        var properties = channel.CreateBasicProperties();
        //        //properties.DeliveryMode = 2; // persistent

        //        _logger.LogTrace("Publishing event to RabbitMQ: {EventName}", eventName);

        //        channel.BasicPublish(
        //            exchange: _options.Value.ExchangeName,
        //            routingKey: eventName,
        //            mandatory: true,
        //            basicProperties: properties,
        //            body: body);
        //    });
        //}
        //catch (Exception ex)
        //{
        //    _logger.LogError(ex, "Publish event:({EventName}) {EventType} failed", eventName, eventDataType);
        //}

        //return Task.CompletedTask;
    }

    public void Subscribe<T, TH>()
        where TH : IEventHandler<T>
    {
        var eventName = _subsManager.GetEventKey<T>();
        DoInternalSubscription(eventName);

        _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}",
            eventName,
            typeof(TH).GetGenericTypeName());

        _subsManager.AddSubscription<T, TH>();
        StartBasicConsume();
    }

    public void Unsubscribe<T, TH>()
        where TH : IEventHandler<T>
    {
        var eventName = _subsManager.GetEventKey<T>();

        _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

        _subsManager.RemoveSubscription<T, TH>();
    }

    private async Task Consumer_Received(object sender,
        BasicDeliverEventArgs eventArgs)
    {
        var eventName = eventArgs.RoutingKey;
        //var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

        try
        {
            //if (message.ToLowerInvariant().Contains("throw-fake-exception"))
            //{
            //    throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
            //}
            var eventData = ParseMessage(eventArgs.Body.Span, eventName);
            if (eventData == null)
            {
                _logger.LogWarning("Can not parse message,eventName={EventName}", eventName);
                return;
            }

            await ProcessEvent(eventName, eventData);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex,
                "----- ERROR Processing message({EventName}) \"{Message}\"",
                eventName,
                Encoding.UTF8.GetString(eventArgs.Body.Span));
        }

        // Even on exception we take the message off the queue.
        // in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX). 
        // For more information see: https://www.rabbitmq.com/dlx.html
        _consumerChannel?.BasicAck(eventArgs.DeliveryTag, false);
    }

    private IModel CreateConsumerChannel()
    {
        if (!_persistentConnection.IsConnected)
        {
            _persistentConnection.TryConnect();
        }

        _logger.LogTrace("Creating RabbitMQ consumer channel");

        var channel = _persistentConnection.CreateModel();

        channel.ExchangeDeclare(_options.Value.ExchangeName,
            durable: true,
            type: "direct");

        channel.QueueDeclare(_options.Value.ClientName,
            true,
            false,
            false,
            null);

        channel.CallbackException += (sender,
            ea) =>
        {
            _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

            _consumerChannel?.Dispose();
            _consumerChannel = CreateConsumerChannel();
            StartBasicConsume();
        };

        return channel;
    }

    private void DoInternalSubscription(string eventName)
    {
        var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
        if (!containsKey)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            _consumerChannel.QueueBind(_options.Value.ClientName,
                _options.Value.ExchangeName,
                eventName);
        }
    }

    private object? ParseMessage(ReadOnlySpan<byte> message,
        string eventName)
    {
        var eventType = _subsManager.GetEventTypeByName(eventName);

        if (eventType == null)
        {
            return null;
        }

        return _rabbitMqSerializer.Deserialize(message, eventType);
    }

    private async Task ProcessEvent(string eventName,
        object? eventData)
    {
        _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);

        if (_subsManager.HasSubscriptionsForEvent(eventName))
        {
            var subscriptions = _subsManager.GetHandlersForEvent(eventName);
            foreach (var subscription in subscriptions)
            {
                {
                    var handler = (IEventHandler)_serviceProvider.GetRequiredService(subscription.HandlerType);

                    //if (handler == null) continue;
                    var eventType = _subsManager.GetEventTypeByName(eventName);
                    if (eventType == null)
                    {
                        _logger.LogWarning("Get event type failed,eventName={EventName}", eventName);
                        continue;
                    }

                    //var eventData = JsonSerializer.Deserialize(message, eventType, _jsonContextProvider.GetJsonSerializerContext());
                    //var eventData = _rabbitMqSerializer.Deserialize(Encoding.UTF8.GetBytes(message), eventType);
                    //var eventData = _rabbitMqSerializer.Deserialize(message, eventType);
                    if (eventData == null)
                    {
                        _logger.LogWarning("Deserialize data to type `{Type}` failed,json data is `{Data}`",
                            eventType,
                            eventData);
                        continue;
                    }

                    await _eventHandlerInvoker.InvokeAsync(handler, eventData, eventType);
                    //var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                    //await Task.Yield();
                    //await (Task)concreteType.GetMethod("HandleAsync").Invoke(handler, new object[] { integrationEvent });
                }
            }
        }
        else
        {
            _logger.LogWarning("No subscription for RabbitMQ event: {EventName}", eventName);
        }
    }

    private Task PublishAsync(Type eventDataType,
        byte[] eventData)
    {
        if (!_persistentConnection.IsConnected)
        {
            _persistentConnection.TryConnect();
        }

        var eventName = _subsManager.GetEventKey(eventDataType);

        var policy = Policy.Handle<BrokerUnreachableException>()
            .Or<SocketException>()
            .WaitAndRetry(_options.Value.RetryCount,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (ex,
                    time) =>
                {
                    _logger.LogWarning(ex,
                        "Could not publish event: {EventType} after {Timeout}s ({ExceptionMessage})",
                        eventData.GetType().Name,
                        $"{time.TotalSeconds:n1}",
                        ex.Message);
                });

        _logger.LogTrace("Creating RabbitMQ channel to publish event:({EventName}) {EventType}",
            eventName,
            eventDataType);

        using var channel = _persistentConnection.CreateModel();
        _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventName} {EventType}",
            eventName,
            eventDataType);

        channel.ExchangeDeclare(_options.Value.ExchangeName, durable: true, type: "direct");
        try
        {
            //var body = JsonSerializer.SerializeToUtf8Bytes(eventData, eventData.GetType(), _jsonContextProvider.GetJsonSerializerContext());
            var body = eventData;

            policy.Execute(() =>
            {
                var properties = channel.CreateBasicProperties();
                //properties.DeliveryMode = 2; // persistent

                _logger.LogTrace("Publishing event to RabbitMQ: {EventName}", eventName);

                channel.BasicPublish(
                    _options.Value.ExchangeName,
                    eventName,
                    true,
                    properties,
                    body);
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Publish event:({EventName}) {EventType} failed", eventName, eventDataType);
        }

        return Task.CompletedTask;
    }

    private byte[] SerializeToBytes<TEventData>(TEventData eventData)
    {
        return _rabbitMqSerializer.Serialize(eventData);
    }

    private void StartBasicConsume()
    {
        _logger.LogTrace("Starting RabbitMQ basic consume");

        if (_consumerChannel != null)
        {
            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

            consumer.Received += Consumer_Received;

            _consumerChannel.BasicConsume(
                _options.Value.ClientName,
                false,
                consumer);
        }
        else
        {
            _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
        }
    }

    private void SubsManager_OnEventRemoved(object? sender,
        string eventName)
    {
        if (!_persistentConnection.IsConnected)
        {
            _persistentConnection.TryConnect();
        }

        using var channel = _persistentConnection.CreateModel();
        channel.QueueUnbind(_options.Value.ClientName,
            _options.Value.ExchangeName,
            eventName);

        if (_subsManager.IsEmpty)
        {
            _consumerChannel?.Close();
        }
    }

    //private async Task ProcessEvent(string eventName, string message)
    //{
    //    _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);

    //    if (_subsManager.HasSubscriptionsForEvent(eventName))
    //    {
    //        var subscriptions = _subsManager.GetHandlersForEvent(eventName);
    //        foreach (var subscription in subscriptions)
    //        {
    //            {
    //                var handler = (IEventHandler)_serviceProvider.GetRequiredService(subscription.HandlerType);

    //                //if (handler == null) continue;
    //                var eventType = _subsManager.GetEventTypeByName(eventName);
    //                if (eventType == null)
    //                {
    //                    _logger.LogWarning("Get event type failed,eventName={EventName}", eventName);
    //                    continue;
    //                }
    //                //var eventData = JsonSerializer.Deserialize(message, eventType, _jsonContextProvider.GetJsonSerializerContext());
    //                //var eventData = _rabbitMqSerializer.Deserialize(Encoding.UTF8.GetBytes(message), eventType);
    //                var eventData = _rabbitMqSerializer.Deserialize(message, eventType);
    //                if (eventData == null)
    //                {
    //                    _logger.LogWarning("Deserialize data to type `{Type}` failed,json data is `{Data}`", eventType, message);
    //                    continue;
    //                }
    //                await _eventHandlerInvoker.InvokeAsync(handler, eventData, eventType);
    //                //var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

    //                //await Task.Yield();
    //                //await (Task)concreteType.GetMethod("HandleAsync").Invoke(handler, new object[] { integrationEvent });
    //            }
    //        }
    //    }
    //    else
    //    {
    //        _logger.LogWarning("No subscription for RabbitMQ event: {EventName}", eventName);
    //    }
    //}
}
