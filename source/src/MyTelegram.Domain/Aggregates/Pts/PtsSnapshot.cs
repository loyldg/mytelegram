namespace MyTelegram.Domain.Aggregates.Pts;

public class PtsSnapshot : ISnapshot
{
    public PtsSnapshot(long peerId,
        int pts,
        int qts,
        int unreadCount,
        int date,
        long globalSeqNo,
        long permAuthKeyId
    )
    {
        PeerId = peerId;
        Pts = pts;
        Qts = qts;
        UnreadCount = unreadCount;
        Date = date;
        GlobalSeqNo = globalSeqNo;
        PermAuthKeyId = permAuthKeyId;
    }

    public int Date { get; }
    public long GlobalSeqNo { get; }
    public long PermAuthKeyId { get; }

    public long PeerId { get; }
    public int Pts { get; }
    public int Qts { get; }
    public int UnreadCount { get; }
}
