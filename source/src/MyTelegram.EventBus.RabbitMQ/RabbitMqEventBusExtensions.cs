namespace MyTelegram.EventBus.RabbitMQ;

public static class RabbitMqEventBusExtensions
{
    public static IServiceCollection AddMyTelegramRabbitMqEventBus(this IServiceCollection services)
    {
        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        services.AddSingleton<IRabbitMqPersistentConnection,DefaultRabbitMqPersistentConnection>();

        services.AddSingleton<IEventBus, EventBusRabbitMq>();
        services.AddSingleton<IEventHandlerInvoker, EventHandlerInvoker>();
        services.AddTransient<IRabbitMqSerializer, Utf8JsonRabbitMqSerializer>();
        //services.AddTransient<IRabbitMqSerializer, MemoryPackRabbitMqSerializer>();

        return services;
    }
}