namespace MyTelegram.Domain.Sagas.Identities;

public class EditChatTitleSagaLocator : ISagaLocator
{
    public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        if (domainEvent.GetAggregateEvent() is not IHasCorrelationId id)
        {
            throw new NotSupportedException(
                $"{domainEvent.GetAggregateEvent().GetType().FullName} should impl IHasCorrelationId.");
        }

        return Task.FromResult<ISagaId>(new EditChatTitleSagaId($"editchattitlesaga-{id.CorrelationId}"));
    }
}
