namespace MyTelegram.ReadModel.ReadModelLocators;

public class DialogReadModelLocator : IDialogReadModelLocator
{
    public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        var aggregateEvent = domainEvent.GetAggregateEvent();
        if (domainEvent.AggregateType == typeof(DialogAggregate))
        {
            yield return domainEvent.GetIdentity().Value;
        }
        else
        {
            switch (aggregateEvent)
            {
                case OutboxMessageCreatedEvent outboxCreatedEvent:
                    //yield return outboxCreatedEvent.DialogId.Value;
                    yield return DialogId.Create(outboxCreatedEvent.OutboxMessageItem.OwnerPeer.PeerId,
                        outboxCreatedEvent.OutboxMessageItem.ToPeer).Value;
                    break;

                case PeerNotifySettingsUpdatedEvent peerNotifySettingsUpdateEvent:
                    yield return DialogId.Create(peerNotifySettingsUpdateEvent.OwnerPeerId,
                        peerNotifySettingsUpdateEvent.PeerType,
                        peerNotifySettingsUpdateEvent.PeerId).Value;

                    break;

                case OutboxMessagePinnedUpdatedEvent outboxPinnedUpdatedEvent:
                    yield return DialogId.Create(outboxPinnedUpdatedEvent.SenderPeerId,
                        outboxPinnedUpdatedEvent.ToPeer).Value;
                    break;
                case InboxMessagePinnedUpdatedEvent inboxPinnedUpdatedEvent:
                    yield return DialogId.Create(inboxPinnedUpdatedEvent.OwnerPeerId,
                        inboxPinnedUpdatedEvent.ToPeer).Value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        $"Not supported domain event:{aggregateEvent.GetType().Name} for DialogReadModelLocator");
            }
        }
    }
}
