namespace MyTelegram.Domain.Events.Pts;

public class PtsForAuthKeyIdUpdatedEvent : AggregateEvent<PtsAggregate, PtsId>
{
    public long PeerId { get; }
    public long PermAuthKeyId { get; }
    public int Pts { get; }
    public int ChangedUnreadCount { get; }
    public long GlobalSeqNo { get; }

    public PtsForAuthKeyIdUpdatedEvent(long peerId,
        long permAuthKeyId,
        int pts,
        int changedUnreadCount,
        long globalSeqNo)
    {
        PeerId = peerId;
        PermAuthKeyId = permAuthKeyId;
        Pts = pts;
        ChangedUnreadCount = changedUnreadCount;
        GlobalSeqNo = globalSeqNo;
    }
}