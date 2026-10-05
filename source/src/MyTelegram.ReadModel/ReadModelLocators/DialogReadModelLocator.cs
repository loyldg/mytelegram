using MyTelegram.Domain.Events.Temp;

namespace MyTelegram.ReadModel.ReadModelLocators;

public class DialogReadModelLocator : IDialogReadModelLocator, ITransientDependency
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
                case UpdateReadChannelOutboxEvent updateReadChannelOutboxEvent:
                    yield return DialogId.Create(updateReadChannelOutboxEvent.MessageSenderUserId, PeerType.Channel,
                        updateReadChannelOutboxEvent.ChannelId).Value;
                    break;

                case OutboxMessageCreatedEvent outboxCreatedEvent:
                    //yield return outboxCreatedEvent.DialogId.Value;
                    yield return DialogId.Create(outboxCreatedEvent.RequestInfo.UserId,
                        outboxCreatedEvent.OutboxMessageItem.ToPeer).Value;

                    break;

                case InboxMessageCreatedEvent inboxMessageCreatedEvent:
                    yield return DialogId.Create(inboxMessageCreatedEvent.InboxMessageItem.OwnerPeer.PeerId,
                        inboxMessageCreatedEvent.InboxMessageItem.ToPeer).Value;
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
                case SendOutboxMessageCompletedSagaEvent sendOutboxMessageCompletedEvent2:
                    if (sendOutboxMessageCompletedEvent2.MessageItem.ToPeer.PeerType == PeerType.Channel)
                    {
                        yield return DialogId.Create(sendOutboxMessageCompletedEvent2.MessageItem.SenderPeer.PeerId,
                            sendOutboxMessageCompletedEvent2.MessageItem.ToPeer).Value;
                    }
                    break;
                case DraftClearedEvent:
                    yield return domainEvent.GetIdentity().Value;
                    break;
                case DraftDeletedEvent draftDeletedEvent:
                    yield return DialogId.Create(draftDeletedEvent.OwnerPeerId, draftDeletedEvent.ToPeer).Value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        $"Not supported domain event: {aggregateEvent.GetType().Name} for DialogReadModelLocator");
            }
        }
    }
}