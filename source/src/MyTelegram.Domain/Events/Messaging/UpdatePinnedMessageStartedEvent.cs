namespace MyTelegram.Domain.Events.Messaging;

public class UpdatePinnedMessageStartedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>, IHasCorrelationId
{
    public UpdatePinnedMessageStartedEvent(RequestInfo requestInfo,
        long ownerPeerId,
        int messageId,
        bool pinned,
        bool pmOneSide,
        bool silent,
        int date,
        bool isOut,
        IReadOnlyList<InboxItem> inboxItems,
        long senderPeerId,
        int senderMessageId,
        Peer toPeer,
        long randomId,
        string messageActionData
    ) : base(requestInfo)
    {
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
        Pinned = pinned;
        PmOneSide = pmOneSide;
        Silent = silent;
        Date = date;
        IsOut = isOut;
        InboxItems = inboxItems;
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
        ToPeer = toPeer;
        RandomId = randomId;
        MessageActionData = messageActionData;
    }

    public long OwnerPeerId { get; }
    public int MessageId { get; }
    public bool Pinned { get; }
    public bool PmOneSide { get; }
    public bool Silent { get; }
    public int Date { get; }
    public bool IsOut { get; }
    public IReadOnlyList<InboxItem> InboxItems { get; }
    public long SenderPeerId { get; }
    public int SenderMessageId { get; }
    public Peer ToPeer { get; }
    public long RandomId { get; }
    public string MessageActionData { get; }
}
