//namespace MyTelegram.Domain.Sagas;

//public class UpdatePinnedMessageSagaSnapshot : ISnapshot
//{
//    public UpdatePinnedMessageSagaSnapshot(long reqMsgId,
//        long selfAuthKeyId,
//        PeerType toPeerType,
//        long toPeerId,
//        bool needWaitForOutboxPinnedUpdated,
//        int inboxCount,
//        int replyToMsgId,
//        // bool receiveOutboxPinnedUpdated,
//        int updatedInboxCount,
//        long startUpdatePinnedOwnerPeerId,
//        long selfUserId,
//        long randomId,
//        string? messageActionData,
//        long senderPeerId,
//        int senderMessageId,
//        bool pinned,
//        bool pmOneSide,
//        bool silent,
//        int date,
//        Guid correlationId,
//        Dictionary<long, PinnedMsgItem> updatePinItems,
//        int pinnedMsgId)
//    {
//        ReqMsgId = reqMsgId;
//        SelfAuthKeyId = selfAuthKeyId;
//        ToPeerType = toPeerType;
//        ToPeerId = toPeerId;
//        NeedWaitForOutboxPinnedUpdated = needWaitForOutboxPinnedUpdated;
//        InboxCount = inboxCount;
//        ReplyToMsgId = replyToMsgId;
//        // ReceiveOutboxPinnedUpdated = receiveOutboxPinnedUpdated;
//        UpdatedInboxCount = updatedInboxCount;
//        StartUpdatePinnedOwnerPeerId = startUpdatePinnedOwnerPeerId;
//        SelfUserId = selfUserId;
//        RandomId = randomId;
//        MessageActionData = messageActionData;
//        SenderPeerId = senderPeerId;
//        SenderMessageId = senderMessageId;
//        Pinned = pinned;
//        PmOneSide = pmOneSide;
//        Silent = silent;
//        Date = date;
//        CorrelationId = correlationId;
//        UpdatePinItems = updatePinItems;
//        PinnedMsgId = pinnedMsgId;
//    }

//    public Guid CorrelationId { get; }
//    public int Date { get; }
//    public int InboxCount { get; }
//    public string? MessageActionData { get; }

//    public bool NeedWaitForOutboxPinnedUpdated { get; }
//    public bool Pinned { get; }

//    public int PinnedMsgId { get; }
//    public bool PmOneSide { get; }
//    public long RandomId { get; }
//    // public bool ReceiveOutboxPinnedUpdated { get; }
//    public int ReplyToMsgId { get; }

//    public long ReqMsgId { get; }
//    public long SelfAuthKeyId { get; }
//    public long SelfUserId { get; }
//    public int SenderMessageId { get; }
//    public long SenderPeerId { get; }
//    public bool Silent { get; }
//    public long StartUpdatePinnedOwnerPeerId { get; }
//    public long ToPeerId { get; }

//    public PeerType ToPeerType { get; }
//    public int UpdatedInboxCount { get; }

//    public Dictionary<long, PinnedMsgItem> UpdatePinItems { get; }
//}
