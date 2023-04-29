namespace MyTelegram.Domain.Sagas.Events;

public class
    UpdatePinnedMessageSagaStartedEvent : RequestAggregateEvent2<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId>,
        IHasCorrelationId
{
    public UpdatePinnedMessageSagaStartedEvent(
        RequestInfo requestInfo,
        bool needWaitForOutboxPinnedUpdated,
        bool pinned,
        bool pmOneSide,
        bool silent,
        int inboxCount,
        int replyToMsgId,
        long ownerPeerId,
        int messageId,
        long senderPeerId,
        int senderMessageId,
        Peer toPeer,
        long randomId,
        int date,
        string? messageActionData,
        Guid correlationId
    ) : base(requestInfo)
    {
        NeedWaitForOutboxPinnedUpdated = needWaitForOutboxPinnedUpdated;
        Pinned = pinned;
        PmOneSide = pmOneSide;
        Silent = silent;
        InboxCount = inboxCount;
        ReplyToMsgId = replyToMsgId;
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
        ToPeer = toPeer;
        RandomId = randomId;
        Date = date;
        MessageActionData = messageActionData;
        CorrelationId = correlationId;
    }

    public int Date { get; }
    public int InboxCount { get; }

    public string? MessageActionData { get; }
    public int MessageId { get; }
    public bool NeedWaitForOutboxPinnedUpdated { get; }

    public long OwnerPeerId { get; }
    public bool Pinned { get; }
    public bool PmOneSide { get; }

    public long RandomId { get; }
    public int ReplyToMsgId { get; }

    public int SenderMessageId { get; }
    public Peer ToPeer { get; }
    public long SenderPeerId { get; }
    public bool Silent { get; }

    //public int MessageId { get; }
    ////public long ChannelId { get; }
    //public bool Pinned { get; }
    //public bool PmOneSide { get; }
    //public bool Silent { get; }
    //public int Date { get; }

    //public bool IsOut { get; }
    //public IReadOnlyList<InboxItem> InboxItems { get; }
    //public long SenderPeerId { get; }
    //public int SenderMessageId { get; }
    //public long ToPeerId { get; }
    public Guid CorrelationId { get; }
}
