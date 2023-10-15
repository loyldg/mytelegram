using Rebus.Handlers;

namespace MyTelegram.EventBus.Rebus;

public class RebusDistributedEventHandlerAdapter<TEventData> : IHandleMessages<TEventData>, IRebusDistributedEventHandlerAdapter
{
    protected RebusEventBus RebusDistributedEventBus { get; }
    private readonly IEventBusSubscriptionsManager _eventBusSubscriptionsManager;

    public RebusDistributedEventHandlerAdapter(RebusEventBus rebusDistributedEventBus, IEventBusSubscriptionsManager eventBusSubscriptionsManager)
    {
        RebusDistributedEventBus = rebusDistributedEventBus;
        _eventBusSubscriptionsManager = eventBusSubscriptionsManager;
    }

    public async Task Handle(TEventData message)
    {
        var eventName = _eventBusSubscriptionsManager.GetEventKey(typeof(TEventData));
        await RebusDistributedEventBus.ProcessEventAsync(eventName, message);
    }
}