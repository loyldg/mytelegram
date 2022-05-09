namespace MyTelegram.Domain.Sagas.Identities;

public class MessageSagaLocator : ISagaLocator
{
    public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        if (domainEvent.GetAggregateEvent() is not IHasCorrelationId id)
        {
            throw new NotSupportedException(
                $"Domain event:{domainEvent.GetAggregateEvent().GetType().FullName} should impl IHasCorrelationId ");
        }

        var messageSagaId = new MessageSagaId($"messagesaga-{id.CorrelationId}");

        return Task.FromResult<ISagaId>(messageSagaId);
    }
}
