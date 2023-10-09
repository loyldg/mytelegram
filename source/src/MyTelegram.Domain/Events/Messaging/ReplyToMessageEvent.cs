namespace MyTelegram.Domain.Events.Messaging;

public class ReplyToMessageEvent : RequestAggregateEvent2<MessageAggregate, MessageId>
{
    public ReplyToMessageEvent(
        RequestInfo requestInfo,
        int senderMessageId,
        IReadOnlyList<InboxItem>? inboxItems) : base(requestInfo)
    {
        SenderMessageId = senderMessageId;
        InboxItems = inboxItems;

    }

    public int SenderMessageId { get; }
    public IReadOnlyList<InboxItem>? InboxItems { get; }

}