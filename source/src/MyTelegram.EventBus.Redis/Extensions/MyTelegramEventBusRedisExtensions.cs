using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MyTelegram.EventBus.Redis.Extensions;

public static class MyTelegramEventBusRedisExtensions
{
    public static IServiceCollection AddMyTelegramRedisEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<EventBusRedisOptions>();
        services.AddOptions<EventBusOptions>()
            .Bind(configuration.GetRequiredSection("EventBus"))
            ;

        services.AddTransient<IRabbitMqSerializer, RabbitMqSerializer>();
        services.AddSingleton<IEventBus, RedisStreamEventBus>();
        services.AddSingleton<IHostedService>(sp => (RedisStreamEventBus)sp.GetRequiredService<IEventBus>());

        return services;
    }
}
