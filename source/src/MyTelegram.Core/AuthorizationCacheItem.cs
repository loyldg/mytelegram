namespace MyTelegram.Core;

public record AuthorizationCacheItem(long UserId, long AccessHashKeyId)
{
    public static string GetCacheKey(string authKeyHash)
    {
        return MyCacheKey.With("authorizations", authKeyHash);
    }
}