using MyTelegram.Domain.Aggregates.PushUpdates;

namespace MyTelegram.ReadModel.ReadModelLocators;

public class PtsReadModelLocator : IPtsReadModelLocator, ITransientDependency
{
    public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        var ownerPeerId = 0L;
        var aggregateEvent = domainEvent.GetAggregateEvent();
        switch (aggregateEvent)
        {
            //case TempPtsIncrementedEvent tempPtsIncrementedEvent:
            //    ownerPeerId = tempPtsIncrementedEvent.OwnerPeerId;
            //    break;

            case PtsGlobalSeqNoUpdatedEvent ptsGlobalSeqNoUpdatedEvent:
                ownerPeerId = ptsGlobalSeqNoUpdatedEvent.PeerId;
                break;

            case UpdatesCreatedEvent updateCreatedEvent:
                ownerPeerId = updateCreatedEvent.OwnerPeerId;
                break;

            case EncryptedPushUpdatesCreatedEvent encryptedPushUpdatesCreatedEvent:
                ownerPeerId = encryptedPushUpdatesCreatedEvent.InboxOwnerPeerId;
                break;

            case PtsUpdatedEvent ptsUpdatedEvent:
                ownerPeerId = ptsUpdatedEvent.PeerId;
                break;
            case InboxMessageEditCompletedSagaEvent inboxEditCompletedEvent:
                ownerPeerId = inboxEditCompletedEvent.NewMessageItem.OwnerPeer.PeerId;
                break;
            case OutboxMessageEditCompletedSagaEvent outboxEditCompletedEvent:
                ownerPeerId = outboxEditCompletedEvent.NewMessageItem.OwnerPeer.PeerId;
                break;
            case ReadHistoryPtsIncrementSagaEvent readHistoryPtsIncrementEvent:
                ownerPeerId = readHistoryPtsIncrementEvent.PeerId;
                break;
            case DeleteMessagePtsIncrementedSagaEvent deleteMessagePtsIncrementedEvent:
                ownerPeerId = deleteMessagePtsIncrementedEvent.UserId;
                break;
            case ClearSingleUserHistoryCompletedSagaEvent clearSingleUserHistoryCompletedEvent:
                ownerPeerId = clearSingleUserHistoryCompletedEvent.DeletedBoxItem.OwnerPeerId;
                break;
            case UserCreatedEvent userCreatedEvent:
                ownerPeerId = userCreatedEvent.UserId;
                break;
            //case UpdatePinnedBoxPtsCompletedSagaEvent updatePinnedBoxPtsCompletedEvent:
            //    ownerPeerId = updatePinnedBoxPtsCompletedEvent.PeerId;
            //    break;
            case ChannelCreatedEvent channelCreatedEvent:
                ownerPeerId = channelCreatedEvent.ChannelId;
                break;
            //case UpdateOutboxPinnedCompletedSagaEvent updateOutboxPinnedCompletedEvent:
            //    ownerPeerId = updateOutboxPinnedCompletedEvent.OwnerPeerId;
            //    break;
        }

        if (ownerPeerId != 0)
        {
            yield return PtsId.Create(ownerPeerId).Value;
        }
    }
}