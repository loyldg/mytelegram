using System.Text.Json;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using MyTelegram.Core;
using StackExchange.Redis;

namespace MyTelegram.Caching.Redis;

public static class MyTelegramCachingExtensions
{
    public static IServiceCollection AddMyTelegramStackExchangeRedisCache(
        this IServiceCollection services,
        Action<RedisCacheOptions>? configureCache = null)
    {
        // Register distributed cache (high-level)
        services.AddStackExchangeRedisCache(options => { configureCache?.Invoke(options); });

        // Register the low-level Redis multiplexer (for atomic ops)
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var opts = new RedisCacheOptions();
            configureCache?.Invoke(opts);

            if (string.IsNullOrWhiteSpace(opts.Configuration))
                throw new InvalidOperationException("Redis connection string not set!");

            var cfg = ConfigurationOptions.Parse(opts.Configuration);

            return ConnectionMultiplexer.Connect(cfg);
        });

        // Register cache manager
        services.AddSingleton(typeof(ICacheManager<>), typeof(CacheManager<>));
        services.AddSingleton<IRedisHelper, RedisHelper>();

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