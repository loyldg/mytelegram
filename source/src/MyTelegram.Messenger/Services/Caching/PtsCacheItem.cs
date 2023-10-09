using Interlocked = System.Threading.Interlocked;

namespace MyTelegram.Messenger.Services.Caching;

public class PtsCacheItem
{
    private int _pts;
    private int _qts;
    private int _unreadCount;
    public PtsCacheItem(long ownerUid, int pts = 0, int qts = 0)
    {
        OwnerPeerId = ownerUid;
        _pts = pts;
        _qts = qts;
    }

    public long OwnerPeerId { get; private set; }

    public int Pts => _pts;

    public int Qts => _qts;
    public int UnreadCount => _unreadCount;
    //public int UnreadCount { get; set; }

    public void IncrementPts()
    {
        Interlocked.Increment(ref _pts);
    }

    public void AddUnreadCount(int unreadCount)
    {
        Interlocked.Add(ref _unreadCount, unreadCount);
    }

    public void AddPts(int ptsCount)
    {
        Interlocked.Add(ref _pts, ptsCount);
    }

    public void AddQts(int qtsCount)
    {
        Interlocked.Add(ref _qts, qtsCount);
    }

    public void IncrementQts()
    {
        Interlocked.Increment(ref _qts);
    }
}