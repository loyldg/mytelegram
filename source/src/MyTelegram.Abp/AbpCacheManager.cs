namespace MyTelegram.Abp;

public class AbpCacheManager<TCacheItem> : ICacheManager<TCacheItem> where TCacheItem : class
{
    private readonly IDistributedCache<TCacheItem> _distributedCache;

    public AbpCacheManager(IDistributedCache<TCacheItem> distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task SetAsync(string key,
        TCacheItem value, int ttlInSeconds = -1)
    {
        DistributedCacheEntryOptions? cacheOptions;
        if (ttlInSeconds > 0)
        {
            cacheOptions = new()
            {
                SlidingExpiration = TimeSpan.FromSeconds(ttlInSeconds)
            };
        }
        else
        {
            cacheOptions = new()
            {
                SlidingExpiration = null
            };
        }

        return _distributedCache.SetAsync(key, value, cacheOptions);
    }

    public Task<TCacheItem?> GetAsync(string key)
    {
        return _distributedCache.GetAsync(key)!;
    }

    public async Task<IDictionary<string, TCacheItem>> GetManyAsync(IReadOnlyList<string> keys)
    {
        var items = await _distributedCache.GetManyAsync(keys).ConfigureAwait(false);
        return items.ToDictionary(k => k.Key, v => v.Value);
    }

    public Task RemoveAsync(string key)
    {
        return _distributedCache.RemoveAsync(key);
    }
}