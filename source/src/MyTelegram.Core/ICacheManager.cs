namespace MyTelegram.Core;

public interface ICacheManager<TCacheItem> where TCacheItem : class
{
    Task<TCacheItem?> GetAsync(string key);
    Task<IDictionary<string, TCacheItem>> GetManyAsync(IReadOnlyList<string> keys);
    Task RemoveAsync(string key);

    Task SetAsync(string key,
        TCacheItem value,
        int ttlInSeconds = -1);
}