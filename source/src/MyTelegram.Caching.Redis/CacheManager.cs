using Microsoft.Extensions.Caching.Distributed;
using MyTelegram.Core;

namespace MyTelegram.Caching.Redis;

public class CacheManager<TCacheItem> : ICacheManager<TCacheItem> where TCacheItem : class
{
    private readonly ICacheSerializer _cacheSerializer;
    private readonly IDistributedCache _distributedCache;

    public CacheManager(IDistributedCache distributedCache,
        ICacheSerializer cacheSerializer)
    {
        _distributedCache = distributedCache;
        _cacheSerializer = cacheSerializer;
    }

    public async Task<TCacheItem?> GetAsync(string key)
    {
        var cachedBytes = await _distributedCache.GetAsync(key);
        if (cachedBytes == null)
        {
            return default;
        }

        return _cacheSerializer.Deserialize<TCacheItem?>(cachedBytes);
        //return JsonSerializer.Deserialize<TCacheItem>(Encoding.UTF8.GetString(cachedBytes), _jsonSerializerOptions);
    }

    public async Task<IDictionary<string, TCacheItem>> GetManyAsync(IReadOnlyList<string> keys)
    {
        var cachedDict = new Dictionary<string, TCacheItem>();
        foreach (var key in keys)
        {
            var cacheItem = await GetAsync(key);
            if (cacheItem == null)
            {
                continue;
            }

            cachedDict.Add(key, cacheItem);
        }

        return cachedDict;
    }

    public Task RemoveAsync(string key)
    {
        return _distributedCache.RemoveAsync(key);
    }

    public Task SetAsync(string key,
        TCacheItem value,
        int ttlInSeconds = -1)
    {
        DistributedCacheEntryOptions? cacheOptions;
        if (ttlInSeconds > 0)
        {
            cacheOptions = new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(ttlInSeconds)
            };
        }
        else
        {
            cacheOptions = new DistributedCacheEntryOptions
            {
                SlidingExpiration = null
            };
        }

        //var bytes = JsonSerializer.SerializeToUtf8Bytes(value, _jsonSerializerOptions);
        var bytes = _cacheSerializer.Serialize(value);
        return _distributedCache.SetAsync(key, bytes, cacheOptions);
    }
}
