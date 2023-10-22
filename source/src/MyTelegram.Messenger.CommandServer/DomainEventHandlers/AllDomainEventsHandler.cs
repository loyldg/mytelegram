namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;
public class AllDomainEventsHandler : ISubscribeSynchronousToAll
{
    private readonly IEventBus _eventBus;
    private readonly IDomainEventMessageFactory _domainEventMessageFactory;
    private readonly ILogger<AllDomainEventsHandler> _logger;

    public AllDomainEventsHandler(IEventBus eventBus, IDomainEventMessageFactory domainEventMessageFactory, ILogger<AllDomainEventsHandler> logger)
    {
        _eventBus = eventBus;
        _domainEventMessageFactory = domainEventMessageFactory;
        _logger = logger;
    }

    public async Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in domainEvents)
        {
            var aggregateEvent = domainEvent.GetAggregateEvent();
            if (aggregateEvent is IHasRequestInfo requestInfo)
            {
                var totalMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - requestInfo.RequestInfo.Date;
                if (totalMilliseconds > 200)
                {
                    _logger.LogWarning("Process domain event '{DomainEvent}' is too slow,time={Timespan}ms,reqMsgId={ReqMsgId}",
                        domainEvent.GetAggregateEvent().GetType().Name,
                        totalMilliseconds,
                        requestInfo.RequestInfo.ReqMsgId);
                }
            }
            var message = _domainEventMessageFactory.CreateDomainEventMessage(domainEvent);
            await _eventBus.PublishAsync(message);
        }
    }
}
