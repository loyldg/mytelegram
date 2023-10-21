using MyTelegram.Messenger.CommandServer.EventHandlers;

namespace MyTelegram.Messenger.CommandServer.Extensions;
public static class MyTelegramMessengerCommandServerExtensions
{
    public static void ConfigureEventBus(this IEventBus eventBus)
    {
        eventBus.Subscribe<MessengerCommandDataReceivedEvent, MessengerEventHandler>();
        eventBus.Subscribe<NewDeviceCreatedEvent, MessengerEventHandler>();
        eventBus.Subscribe<BindUidToAuthKeyIntegrationEvent, MessengerEventHandler>();
        eventBus.Subscribe<AuthKeyUnRegisteredIntegrationEvent, MessengerEventHandler>();

    }

    public static void AddMyTelegramMessengerCommandServer(this IServiceCollection services,
        Action<IEventFlowOptions>? configure = null)
    {
        //services.AddTransient<PtsEventHandler>();
        services.AddTransient<MessengerEventHandler>();
        //services.AddTransient<IReadModelUpdateManager, MyTelegramCommandServerReadModelUpdateManager>();
    }
}
