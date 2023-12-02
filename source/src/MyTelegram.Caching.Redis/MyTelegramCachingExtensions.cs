using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using MyTelegram.Core;
using System.Text.Json;

namespace MyTelegram.Caching.Redis;

public static class MyTelegramCachingExtensions
{
    public static IServiceCollection AddMyTelegramStackExchangeRedisCache(this IServiceCollection services,
        Action<RedisCacheOptions>? options = null)
    {
        //services.AddTransient<ICacheSerializer, CacheSerializer>();
        services.AddSingleton(typeof(ICacheManager<>), typeof(CacheManager<>));
        services.AddStackExchangeRedisCache(redisOptions => { options?.Invoke(redisOptions); });
        return services;
    }

    public static IServiceCollection AddCacheJsonSerializer(this IServiceCollection services,
        Action<JsonSerializerOptions>? configure = null)
    {
        var options = new JsonSerializerOptions(JsonSerializerOptions.Default);
        var serializer = new CacheSerializer(options);
        services.AddTransient<ICacheSerializer>(_ => serializer);

        configure?.Invoke(options);

        return services;
    }
}
