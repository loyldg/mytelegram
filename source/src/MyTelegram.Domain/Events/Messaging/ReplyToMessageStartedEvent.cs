namespace MyTelegram.Domain.Events.Messaging;

public class ReplyToMessageStartedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
{
    public bool IsOut { get; }
    public IReadOnlyList<InboxItem> InboxItems { get; }
    public Peer SenderPeer { get; }
    public Peer ToPeer { get; }
    public int SenderMessageId { get; }
    public Guid CorrelationId { get; }

    public ReplyToMessageStartedEvent(bool isOut, IReadOnlyList<InboxItem> inboxItems,
        Peer senderPeer,
        Peer toPeer,
        int senderMessageId,
        Guid correlationId
    )
    {
        IsOut = isOut;
        InboxItems = inboxItems;
        SenderPeer = senderPeer;
        ToPeer = toPeer;
        SenderMessageId = senderMessageId;
        CorrelationId = correlationId;
    }
}