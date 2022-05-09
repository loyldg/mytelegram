namespace MyTelegram.ReadModel.ReadModelLocators;

public class MessageIdLocator : IMessageIdLocator
{
    public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        var aggregateEvent = domainEvent.GetAggregateEvent();
        if (domainEvent.AggregateType == typeof(MessageAggregate))
        {
            yield return domainEvent.GetIdentity().Value;
        }
        else if (domainEvent.AggregateType == typeof(MessageSaga))
        {
            switch (aggregateEvent)
            {
                case SendOutboxMessageCompletedEvent sendOutboxMessageSuccessEvent:
                    yield return MessageId.Create(sendOutboxMessageSuccessEvent.MessageItem.OwnerPeer.PeerId,
                        sendOutboxMessageSuccessEvent.MessageItem.MessageId).Value;
                    break;
                case ReceiveInboxMessageCompletedEvent receiveInboxMessageSuccessEvent:
                    yield return MessageId.Create(receiveInboxMessageSuccessEvent.MessageItem.OwnerPeer.PeerId,
                        receiveInboxMessageSuccessEvent.MessageItem.MessageId).Value;
                    break;
            }
        }
    }
}