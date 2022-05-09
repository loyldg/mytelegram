namespace MyTelegram.Core;

public static class MyTelegramCoreExtensions
{
    public static IServiceCollection AddMyTelegramCoreServices(this IServiceCollection services)
    {
        services.AddSingleton<IAppSettingManager, AppSettingManager>();
        services.AddTransient<IRandomHelper, RandomHelper>();
        services.AddTransient<IHashHelper, HashHelper>();
        services.AddTransient<IClock, UtcClock>();

        services.AddSingleton<IObjectMapper, DefaultObjectMapper>();
        services.AddSingleton(typeof(IObjectMapper<>), typeof(DefaultObjectMapper<>));


        return services;
    }
}