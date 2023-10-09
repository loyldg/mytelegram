namespace MyTelegram.Domain.Events.Messaging;

public class InboxMessageCreatedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>
{
    public MessageItem InboxMessageItem { get; }
    public int SenderMessageId { get; }


    public InboxMessageCreatedEvent(
        RequestInfo requestInfo,
        MessageItem inboxMessageItem, int senderMessageId) : base(requestInfo)
    {
        InboxMessageItem = inboxMessageItem;
        SenderMessageId = senderMessageId;

    }
}