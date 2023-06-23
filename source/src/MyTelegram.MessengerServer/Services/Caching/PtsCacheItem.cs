namespace MyTelegram.MessengerServer.Services.Caching;

public class PtsCacheItem
{
    private int _pts;
    private int _qts;

    public PtsCacheItem(int pts = 0,
        int qts = 0)
    {
        _pts = pts;
        _qts = qts;
    }

    public int Pts => _pts;

    public int Qts => _qts;

    public void AddPts(int ptsCount)
    {
        Interlocked.Add(ref _pts, ptsCount);
    }

    public void IncrementPts()
    {
        Interlocked.Increment(ref _pts);
    }

    public void IncrementQts()
    {
        Interlocked.Increment(ref _qts);
    }
}