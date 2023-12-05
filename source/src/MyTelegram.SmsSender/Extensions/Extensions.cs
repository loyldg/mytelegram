namespace MyTelegram.SmsSender.Extensions;
public static class Extensions
{
    public static IServiceCollection AddMyTelegramSmsSender(this IServiceCollection services)
    {
        services.AddTransient<AppCodeEventHandler>();
        services.AddTransient<ISmsSender, TwilioSmsSender>();

        return services;
    }

    public static void ConfigureEventBus(this IEventBus eventBus)
    {
        eventBus.Subscribe<AppCodeCreatedIntegrationEvent,AppCodeEventHandler>();
    }

    public static Task SendAsync(this ISmsSender smsSender, string phoneNumber, string text)
    {
        return smsSender.SendAsync(new SmsMessage(phoneNumber, text));
    }
}
