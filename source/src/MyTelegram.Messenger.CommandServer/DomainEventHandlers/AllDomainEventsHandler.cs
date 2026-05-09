namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;

public class AllDomainEventsHandler(
    IMessageQueueProcessor<IReadOnlyCollection<IDomainEvent>> domainEventsMessageQueueProcessor)
    : ISubscribeSynchronousToAll
{
    public Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        var queueKey = 0L;
        foreach (var domainEvent in domainEvents)
        {
            var aggregateEvent = domainEvent.GetAggregateEvent();
            if (aggregateEvent is IHasRequestInfo hasRequestInfo)
            {
                queueKey = hasRequestInfo.RequestInfo.PermAuthKeyId;
            }
        }

        domainEventsMessageQueueProcessor.Enqueue(domainEvents, queueKey);

        return Task.CompletedTask;
    }
}