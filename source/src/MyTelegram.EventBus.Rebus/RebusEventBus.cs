using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Transport;

namespace MyTelegram.EventBus.Rebus;

public class RebusEventBus : IEventBus
{
    private readonly IBus _bus;
    private readonly IEventBusSubscriptionsManager _subsManager;
    private readonly ILogger<RebusEventBus> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventHandlerInvoker _eventHandlerInvoker;

    public RebusEventBus(IBus bus, IEventBusSubscriptionsManager subsManager, ILogger<RebusEventBus> logger, IServiceProvider serviceProvider, IEventHandlerInvoker eventHandlerInvoker)
    {
        _bus = bus;
        _subsManager = subsManager;
        _logger = logger;
        _serviceProvider = serviceProvider;
        _eventHandlerInvoker = eventHandlerInvoker;
    }

    public async Task PublishAsync<TEventData>(TEventData eventData) where TEventData : class
    {
        using var scope = new RebusTransactionScope();
        {
            await _bus.Publish(eventData);
            await scope.CompleteAsync();
        }
    }

    public async Task PublishAsync(Type eventDataType, object eventData)
    {
        using var scope = new RebusTransactionScope();
        {
            await _bus.Publish(eventData);
            await scope.CompleteAsync();
        }
    }

    public void Subscribe<TEvent, TEventHandler>() where TEventHandler : IEventHandler<TEvent>
    {
        var eventName = _subsManager.GetEventKey<TEvent>();
        DoInternalSubscription(eventName);

        _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}",
            eventName,
            typeof(TEventHandler).GetGenericTypeName());

        _subsManager.AddSubscription<TEvent, TEventHandler>();

        _bus.Subscribe<TEvent>();
    }

    public void Unsubscribe<TEvent, TEventHandler>() where TEventHandler : IEventHandler<TEvent>
    {
        var eventName = _subsManager.GetEventKey<TEvent>();
        _subsManager.RemoveSubscription<TEvent, TEventHandler>();
        _logger.LogInformation("Unsubscribing from event {EventName}", eventName);


        _bus.Unsubscribe<TEvent>();
    }

    public async Task ProcessEventAsync(string eventName,
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

                    var eventType = _subsManager.GetEventTypeByName(eventName);
                    if (eventType == null)
                    {
                        _logger.LogWarning("Get event type failed,eventName={EventName}", eventName);
                        continue;
                    }
                    if (eventData == null)
                    {
                        _logger.LogWarning("Deserialize data to type `{Type}` failed,json data is `{Data}`",
                            eventType,
                            eventData);
                        continue;
                    }

                    await _eventHandlerInvoker.InvokeAsync(handler, eventData, eventType);
                }
            }
        }
        else
        {
            _logger.LogWarning("No subscription for RabbitMQ event: {EventName}", eventName);
        }
    }

    private void DoInternalSubscription(string eventName)
    {

    }
}