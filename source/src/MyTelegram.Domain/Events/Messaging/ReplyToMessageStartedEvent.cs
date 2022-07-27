namespace MyTelegram.Domain.Events.Messaging;

public class ReplyToMessageStartedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
{
    public int ReplyToMsgId { get; }
    public bool IsOut { get; }
    public IReadOnlyList<InboxItem> InboxItems { get; }
    public Peer OwnerPeer { get; }
    public Peer SenderPeer { get; }
    public Peer ToPeer { get; }
    public int SenderMessageId { get; }
    public long? SavedFromPeerId { get; }
    public int? SavedFromMsgId { get; }
    public IReadOnlyCollection<Peer> RecentRepliers { get; }
    public Guid CorrelationId { get; }

    public ReplyToMessageStartedEvent(int replyToMsgId, bool isOut, IReadOnlyList<InboxItem> inboxItems,
        Peer ownerPeer,
        Peer senderPeer,
        Peer toPeer,
        int senderMessageId,
        long? savedFromPeerId,
        int? savedFromMsgId,
        IReadOnlyCollection<Peer> recentRepliers,
        Guid correlationId)
    {
        ReplyToMsgId = replyToMsgId;
        IsOut = isOut;
        InboxItems = inboxItems;
        OwnerPeer = ownerPeer;
        SenderPeer = senderPeer;
        ToPeer = toPeer;
        SenderMessageId = senderMessageId;
        SavedFromPeerId = savedFromPeerId;
        CorrelationId = correlationId;
        SavedFromMsgId = savedFromMsgId;
        RecentRepliers = recentRepliers;
    }
}