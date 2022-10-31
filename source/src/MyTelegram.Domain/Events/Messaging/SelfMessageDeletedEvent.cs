namespace MyTelegram.Domain.Events.Messaging;

public class SelfMessageDeletedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
{
    public SelfMessageDeletedEvent(long ownerPeerId,
        int messageId,
        bool isOut,
        long senderPeerId,
        int senderMessageId,
        IReadOnlyList<InboxItem>? inboxItems,
        Guid correlationId)
    {
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
        IsOut = isOut;
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
        InboxItems = inboxItems;
        CorrelationId = correlationId;
    }


    public IReadOnlyList<InboxItem>? InboxItems { get; }
    public bool IsOut { get; }
    public int MessageId { get; }

    public long OwnerPeerId { get; }
    public int SenderMessageId { get; }
    public long SenderPeerId { get; }
    public Guid CorrelationId { get; }
}