using MyTelegram.Domain.Aggregates.Temp;

namespace MyTelegram.ReadModel.ReadModelLocators;

public class DraftReadModelLocator : IReadModelLocator, ITransientDependency
{
    public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        var aggregateEvent = domainEvent.GetAggregateEvent();
        switch (aggregateEvent)
        {
            case DraftSavedEvent:
            case DraftClearedEvent:
                yield return domainEvent.GetIdentity().Value;
                break;
            case DraftDeletedEvent draftDeletedEvent:
                yield return DialogId.Create(draftDeletedEvent.OwnerPeerId, draftDeletedEvent.ToPeer).Value;
                break;
        }
    }
}