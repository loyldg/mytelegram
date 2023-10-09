namespace MyTelegram.Domain.Events.Messaging;

public class SelfMessageDeletedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>
{
    public SelfMessageDeletedEvent(
        RequestInfo requestInfo,
        long ownerPeerId,
        int messageId,
        bool isOut,
        long senderPeerId,
        int senderMessageId,
        IReadOnlyList<InboxItem>? inboxItems) : base(requestInfo)
    {
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
        IsOut = isOut;
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
        InboxItems = inboxItems;

    }


    public IReadOnlyList<InboxItem>? InboxItems { get; }
    public bool IsOut { get; }
    public int MessageId { get; }

    public long OwnerPeerId { get; }
    public int SenderMessageId { get; }
    public long SenderPeerId { get; }

}