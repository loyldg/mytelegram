using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MyTelegram.EventBus.RabbitMQ.Extensions;

public static class RabbitMqEventBusExtensions
{
    public static IServiceCollection AddMyTelegramRabbitMqEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<EventBusOptions>()
            .Bind(configuration.GetRequiredSection("EventBus"));
        services.AddOptions<RabbitMqOptions>()
            .Bind(configuration.GetRequiredSection("RabbitMQ:Connections:Default"));

        services.AddTransient<IRabbitMqSerializer, RabbitMqSerializer>();
        services.AddSingleton<IEventBus, RabbitMqEventBus>();
        services.AddSingleton<IHostedService>(sp => (RabbitMqEventBus)sp.GetRequiredService<IEventBus>());

        return services;
    }
}
