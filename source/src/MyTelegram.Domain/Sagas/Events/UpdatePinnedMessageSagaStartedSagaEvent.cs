//namespace MyTelegram.Domain.Sagas.Events;

//public class
//    UpdatePinnedMessageSagaStartedSagaEvent(
//        RequestInfo requestInfo,
//        bool needWaitForOutboxPinnedUpdated,
//        bool pinned,
//        bool pmOneSide,
//        bool silent,
//        int inboxCount,
//        int replyToMsgId,
//        long ownerPeerId,
//        int messageId,
//        long senderPeerId,
//        int senderMessageId,
//        Peer toPeer,
//        long randomId,
//        int date,
//        IMessageAction? messageAction)
//    : RequestAggregateEvent2<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId>(requestInfo)
//{
//    public int Date { get; } = date;
//    public IMessageAction? MessageAction { get; } = messageAction;
//    public int InboxCount { get; } = inboxCount;

//    public int MessageId { get; } = messageId;
//    public bool NeedWaitForOutboxPinnedUpdated { get; } = needWaitForOutboxPinnedUpdated;

//    public long OwnerPeerId { get; } = ownerPeerId;
//    public bool Pinned { get; } = pinned;
//    public bool PmOneSide { get; } = pmOneSide;

//    public long RandomId { get; } = randomId;
//    public int ReplyToMsgId { get; } = replyToMsgId;

//    public int SenderMessageId { get; } = senderMessageId;
//    public Peer ToPeer { get; } = toPeer;
//    public long SenderPeerId { get; } = senderPeerId;
//    public bool Silent { get; } = silent;
//}
