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

        context.Services.UseMyTelegramMessengerServer(options =>
        {
            var connectionString = configuration.GetRequiredSection("ConnectionStrings:Default").Value;
            var databaseName = configuration["App:DatabaseName"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("ConnectionString is null,please set its value in appsettings.json");
            }

            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentException("DatabaseName is null,please set its value in appsettings.json");
            }

            options.ConfigureMongoDb(connectionString, databaseName);
        });

        context.Services.AddHostedService<MyTelegramAbpHostedService>();
        context.Services.AddHostedService<MyTelegramMessengerServerInitBackgroundService>();

        context.Services.AddHostedService<DataProcessorBackgroundService>();
        context.Services.AddHostedService<ObjectMessageSenderBackgroundService>();
    }
}
