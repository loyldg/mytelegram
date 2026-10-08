namespace MyTelegram.Core;

public record UserStatusCacheItem(long UserId, bool IsOnline, int LastUpdateDate)
{
    public static string GetCacheKey(long userId)
    {
        return MyCacheKey.With("user", "status", userId.ToString());
    }
}