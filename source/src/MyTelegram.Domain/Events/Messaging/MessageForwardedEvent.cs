namespace MyTelegram.Domain.Events.Messaging;

public class MessageForwardedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>
{
    public long RandomId { get; }
    public MessageItem OriginalMessageItem { get; }


    public MessageForwardedEvent(RequestInfo requestInfo, long randomId, MessageItem originalMessageItem) : base(requestInfo)
    {
        RandomId = randomId;
        OriginalMessageItem = originalMessageItem;

    }
}