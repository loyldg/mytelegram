namespace MyTelegram.Core;

public record FutureSaltCacheItem(long Salt, int ValidSince, int ValidUntil)
{
    public static string GetCacheKey(long tempAuthKeyId) => MyCacheKey.With("futuresalt", $"{tempAuthKeyId}");
}