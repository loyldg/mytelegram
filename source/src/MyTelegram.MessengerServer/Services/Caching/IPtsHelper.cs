namespace MyTelegram.MessengerServer.Services.Caching;

public interface IPtsHelper
{
    int GetCachedPts(long ownerId);
    Task IncrementPtsAsync(long ownerId, int currentPts, int ptsCount = 1);
}