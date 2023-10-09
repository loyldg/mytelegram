namespace MyTelegram.Messenger.Services.Caching;

public interface IPtsHelper
{
    int GetCachedPts(long ownerId);
    Task<int> IncrementPtsAsync(long ownerId, int currentPts, int ptsCount = 1, long permAuthKeyId = 0, int newUnreadCount = 0);
    Task<int> IncrementQtsAsync(long ownerId, int currentQts, int qtsCount = 1, long permAuthKeyId = 0);
    Task SyncCachedPtsToReadModelAsync(long ownerId);
}