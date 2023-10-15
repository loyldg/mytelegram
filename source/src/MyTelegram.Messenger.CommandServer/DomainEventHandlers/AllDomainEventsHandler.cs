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
                var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - requestInfo.RequestInfo.Date;
                if (timestamp > 100)
                {
                    _logger.LogWarning("{Event} {Timestamp}", aggregateEvent.GetType().Name, timestamp);
                }
            }
            var message = _domainEventMessageFactory.CreateDomainEventMessage(domainEvent);
            await _eventBus.PublishAsync(message);
        }
    }
}
