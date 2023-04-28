using Microsoft.Extensions.Caching.Distributed;
using MyTelegram.Core;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;

namespace MyTelegram.Caching.Redis;

public static class MyTelegramCachingExtensions
{
    public static IServiceCollection AddMyTelegramStackExchangeRedisCache(this IServiceCollection services, Action<RedisCacheOptions>? options = null)
    {
        services.AddTransient<ICacheSerializer, CacheSerializer>();
        services.AddSingleton(typeof(ICacheManager<>), typeof(CacheManager<>));
        services.AddStackExchangeRedisCache(redisOptions =>
        {
            options?.Invoke(redisOptions);
        });
        return services;
    }
}

public class CacheSerializer : ICacheSerializer
{
    //private readonly IJsonSerializer _jsonSerializer;

    //public CacheSerializer(IJsonSerializer jsonSerializer)
    //{
    //    _jsonSerializer = jsonSerializer;
    //}

    public byte[] Serialize<T>(T obj)
    {
        //return Encoding.UTF8.GetBytes(_jsonSerializer.Serialize(obj));
        return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(obj);
    }

    public T? Deserialize<T>(byte[] bytes)
    {
        //return _jsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(bytes));
        return System.Text.Json.JsonSerializer.Deserialize<T>(bytes);
    }
}

public class CacheManager<TCacheItem> : ICacheManager<TCacheItem> where TCacheItem : class
{
    private readonly IDistributedCache _distributedCache;
    private readonly ICacheSerializer _cacheSerializer;

    public CacheManager(IDistributedCache distributedCache,
        ICacheSerializer cacheSerializer)
    {
        _distributedCache = distributedCache;
        _cacheSerializer = cacheSerializer;
    }

    public async Task<TCacheItem?> GetAsync(string key)
    {
        var cachedBytes = await _distributedCache.GetAsync(key).ConfigureAwait(false);
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
            var cacheItem = await GetAsync(key).ConfigureAwait(false);
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
            cacheOptions = new()
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