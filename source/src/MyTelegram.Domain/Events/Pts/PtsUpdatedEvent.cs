namespace MyTelegram.Domain.Events.Pts;

public class PtsUpdatedEvent : AggregateEvent<PtsAggregate, PtsId>
{
    public PtsUpdatedEvent(long peerId,
        long permAuthKeyId,
        int newPts,
        int date,
        long globalSeqNo,
        int changedUnreadCount
        //bool incrementUnreadCount
    )
    {
        PeerId = peerId;
        PermAuthKeyId = permAuthKeyId;
        NewPts = newPts;
        Date = date;
        GlobalSeqNo = globalSeqNo;
        ChangedUnreadCount = changedUnreadCount;
        //IncrementUnreadCount = incrementUnreadCount;
    }

    public int NewPts { get; }
    public int Date { get; }
    public long GlobalSeqNo { get; }

    public int ChangedUnreadCount { get; }
    //public bool IncrementUnreadCount { get; }

    public long PeerId { get; }
    public long PermAuthKeyId { get; }
}