namespace MyTelegram.EventBus.RabbitMQ;

public static class RabbitMqEventBusExtensions
{
    public static IServiceCollection AddMyTelegramRabbitMqEventBus(this IServiceCollection services)
    {
        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        services.AddSingleton<IEventHandlerInvoker, EventHandlerInvoker>();

        services.AddSingleton<IEventBus, EventBusRabbitMq>();

        services.AddSingleton<IRabbitMqPersistentConnection, DefaultRabbitMqPersistentConnection>();
        services.AddTransient<IRabbitMqConnectionFactory, RabbitMqConnectionFactory>();

        services.AddRabbitMqJsonSerializer();

        return services;
    }

    public static IServiceCollection AddRabbitMqJsonSerializer(this IServiceCollection services, Action<JsonSerializerOptions>? configureOptions = null)
    {
        var options = new JsonSerializerOptions(JsonSerializerOptions.Default);
        configureOptions?.Invoke(options);
        services.AddTransient<IRabbitMqSerializer>(_ => new Utf8JsonRabbitMqSerializer(options));

        return services;
    }
}