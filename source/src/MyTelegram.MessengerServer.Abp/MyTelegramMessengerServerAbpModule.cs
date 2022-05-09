namespace MyTelegram.MessengerServer.Abp;

[DependsOn(
    typeof(MyTelegramAbpModule),
    typeof(AbpAutofacModule))
]
public class MyTelegramMessengerServerAbpModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        context.Services.Configure<MyTelegramMessengerServerOptions>(
            configuration.GetRequiredSection(nameof(MyTelegramMessengerServerOptions)));

        context.Services.UseMyTelegramMessengerServer(options =>
        {
            options.ConfigureMongoDb(configuration.GetConnectionString("Default"), "tg-messenger");
        });

        context.Services.AddHostedService<MyTelegramAbpHostedService>();
        context.Services.AddHostedService<MyTelegramMessengerServerInitBackgroundService>();

        context.Services.AddHostedService<DataProcessorBackgroundService>();
        context.Services.AddHostedService<ObjectMessageSenderBackgroundService>();
    }
}
