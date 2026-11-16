using MyTelegram.EventBus.Extensions;
using MyTelegram.MTProto.Extensions;

namespace MyTelegram.GatewayServer.Extensions;

public static class MyTelegramGatewayServerExtensions
{
    public static void AddMyTelegramGatewayServer(this IServiceCollection services)
    {
        services.RegisterServices(typeof(MyTelegramGatewayServerExtensions).Assembly);
        services.AddMyTelegramMtProto();
        services.AddMyTelegramCoreServices();
        services.AddEventHandlers();
    }

    public static void AddEventHandlers(this IServiceCollection services)
    {
        services.AddSubscription<EncryptedMessageResponse, EncryptedMessageResponseEventHandler>();
        services.AddSubscription<UnencryptedMessageResponse, UnencryptedMessageResponseEventHandler>();
        services.AddSubscription<AuthKeyNotFoundEvent, AuthKeyNotFoundEventHandler>();
        services.AddSubscription<TransportErrorEvent, TransportErrorEventHandler>();
        services.AddSubscription<PingTimeoutEvent, PingTimeoutEventHandler>();
        services.AddSubscription<QuickAckEvent, QuickAckEventHandler>();
    }
}