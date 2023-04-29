namespace MyTelegram.Domain.Sagas.Events;

public class UpdatePinnedMessageCompletedEvent : AggregateEvent<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId>
{
    public UpdatePinnedMessageCompletedEvent(
        long reqMsgId,
        bool shouldReplyRpcResult,
        long senderPeerId,
        long ownerPeerId,
        int messageId,
        bool pinned,
        bool pmOneSide,
        int pts,
        Peer toPeer,
        int date)
    {
        ReqMsgId = reqMsgId;
        ShouldReplyRpcResult = shouldReplyRpcResult;
        SenderPeerId = senderPeerId;
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
        Pinned = pinned;
        PmOneSide = pmOneSide;
        Pts = pts;
        ToPeer = toPeer;
        Date = date;
    }

    public int Date { get; }
    public int MessageId { get; }
    public long OwnerPeerId { get; }
    public bool Pinned { get; }
    public bool PmOneSide { get; }
    public int Pts { get; }
    public Peer ToPeer { get; }

    public long ReqMsgId { get; }
    public long SenderPeerId { get; }
    public bool ShouldReplyRpcResult { get; }
}
