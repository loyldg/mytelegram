//namespace MyTelegram.Domain.Sagas.Events;

//public class UpdatePinnedMessageCompletedSagaEvent(
//    RequestInfo requestInfo,
//    bool shouldReplyRpcResult,
//    long senderPeerId,
//    long ownerPeerId,
//    int messageId,
//    bool pinned,
//    bool pmOneSide,
//    int pts,
//    Peer toPeer,
//    int date)
//    : RequestAggregateEvent2<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId>(requestInfo)
//{
//    public int Date { get; } = date;
//    public int MessageId { get; } = messageId;
//    public long OwnerPeerId { get; } = ownerPeerId;
//    public bool Pinned { get; } = pinned;
//    public bool PmOneSide { get; } = pmOneSide;
//    public int Pts { get; } = pts;
//    public Peer ToPeer { get; } = toPeer;
//    public long SenderPeerId { get; } = senderPeerId;
//    public bool ShouldReplyRpcResult { get; } = shouldReplyRpcResult;
//}