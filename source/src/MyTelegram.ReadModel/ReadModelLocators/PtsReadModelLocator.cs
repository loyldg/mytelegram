namespace MyTelegram.ReadModel.ReadModelLocators;

public class PtsReadModelLocator : IPtsReadModelLocator
{
    public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        var ownerPeerId = 0L;
        var aggregateEvent = domainEvent.GetAggregateEvent();
        switch (aggregateEvent)
        {
            case PtsGlobalSeqNoUpdatedEvent ptsGlobalSeqNoUpdatedEvent:
                ownerPeerId = ptsGlobalSeqNoUpdatedEvent.PeerId;
                break;

            case PushUpdatesCreatedEvent pushUpdatesCreatedEvent:
                ownerPeerId = pushUpdatesCreatedEvent.ToPeer.PeerId;
                break;

            case EncryptedPushUpdatesCreatedEvent encryptedPushUpdatesCreatedEvent:
                ownerPeerId = encryptedPushUpdatesCreatedEvent.InboxOwnerPeerId;
                break;

            case PtsUpdatedEvent ptsUpdatedEvent:
                ownerPeerId = ptsUpdatedEvent.PeerId;
                break;
            case InboxMessageEditCompletedEvent inboxEditCompletedEvent:
                ownerPeerId = inboxEditCompletedEvent.OwnerPeerId;
                break;
            case OutboxMessageEditCompletedEvent outboxEditCompletedEvent:
                ownerPeerId = outboxEditCompletedEvent.OwnerPeerId;
                break;
            case ReadHistoryPtsIncrementEvent readHistoryPtsIncrementEvent:
                ownerPeerId = readHistoryPtsIncrementEvent.PeerId;
                break;
            case DeleteMessagePtsIncrementedEvent deleteMessagePtsIncrementedEvent:
                ownerPeerId = deleteMessagePtsIncrementedEvent.PeerId;
                break;
            case ClearSingleUserHistoryCompletedEvent clearSingleUserHistoryCompletedEvent:
                ownerPeerId = clearSingleUserHistoryCompletedEvent.DeletedBoxItem.OwnerPeerId;
                break;

            //case SendOutboxMessageSuccessEvent sendOutboxMessageSuccessEvent:
            //    ownerPeerId = sendOutboxMessageSuccessEvent.OwnerPeerId;
            //    break;
            //case ReceiveInboxMessageSuccessEvent receiveInboxMessageSuccessEvent:
            //    ownerPeerId = receiveInboxMessageSuccessEvent.OwnerPeerId;
            //    break;
            case UserCreatedEvent userCreatedEvent:
                ownerPeerId = userCreatedEvent.UserId;
                break;
            //case OutboxCreatedEvent outboxCreatedEvent:
            //    ownerPeerId = outboxCreatedEvent.OwnerPeerId;
            //    break;
            //case InboxCreatedEvent inboxCreatedEvent:
            //    ownerPeerId = inboxCreatedEvent.OwnerPeerId;
            //    break;
            //case ReadInboxMessage2Event readInboxMessage2Event:
            //    ownerPeerId = readInboxMessage2Event.OwnerPeerId;
            //    break;
            //case OutboxMessageHasReadEvent outboxMessageHasReadEvent:
            //    ownerPeerId = outboxMessageHasReadEvent.OwnerPeerId;
            //    break;
            //case MessageDeletedEvent messageDeletedEvent:
            //    ownerPeerId = messageDeletedEvent.OwnerPeerId;
            //    break;
            //case OtherPartyMessageDeletedEvent otherPartyMessageDeletedEvent:
            //    ownerPeerId = otherPartyMessageDeletedEvent.OwnerPeerId;
            //    break;
            //case OutboxEditedEvent outboxEditedEvent:
            //    ownerPeerId = outboxEditedEvent.OwnerPeerId;
            //    break;
            //case InboxEditedEvent inboxEditedEvent:
            //    ownerPeerId = inboxEditedEvent.InboxOwnerPeerId;
            //    break;
            //case HistoryClearedEvent historyClearedEvent:
            //    ownerPeerId = historyClearedEvent.OwnerPeerId;
            //    break;
            //case ParticipantHistoryClearedEvent participantHistoryClearedEvent:
            //    ownerPeerId = participantHistoryClearedEvent.OwnerPeerId;
            //    break;
            case UpdatePinnedBoxPtsCompletedEvent updatePinnedBoxPtsCompletedEvent:
                ownerPeerId = updatePinnedBoxPtsCompletedEvent.PeerId;
                break;
            //case EncryptedInboxCreatedEvent encryptedInboxCreatedEvent:
            //    ownerPeerId = encryptedInboxCreatedEvent.SenderPeerId; // increment self qts
            //    break;

            case ChannelCreatedEvent channelCreatedEvent:
                ownerPeerId = channelCreatedEvent.ChannelId;
                break;
            case UpdateOutboxPinnedCompletedEvent updateOutboxPinnedCompletedEvent:
                ownerPeerId = updateOutboxPinnedCompletedEvent.OwnerPeerId;
                break;
        }

        if (ownerPeerId != 0)
        {
            yield return PtsId.Create(ownerPeerId).Value;
        }
    }
}