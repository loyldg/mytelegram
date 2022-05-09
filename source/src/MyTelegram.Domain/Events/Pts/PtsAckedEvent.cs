namespace MyTelegram.Domain.Events.Pts;

public class PtsAckedEvent : AggregateEvent<PtsAggregate, PtsId>
{
    public PtsAckedEvent(long peerId,
        long permAuthKeyId,
        long msgId,
        int pts,
        long globalSeqNo,
        Peer toPeer)
    {
        PeerId = peerId;
        PermAuthKeyId = permAuthKeyId;
        MsgId = msgId;
        Pts = pts;
        GlobalSeqNo = globalSeqNo;
        ToPeer = toPeer;
    }

    public long GlobalSeqNo { get; }
    public long MsgId { get; }
    public long PeerId { get; }
    public long PermAuthKeyId { get; }
    public int Pts { get; }
    public Peer ToPeer { get; }
}
