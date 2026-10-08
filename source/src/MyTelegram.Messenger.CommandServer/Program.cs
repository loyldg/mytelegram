using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyTelegram.Caching.Redis;
using MyTelegram.Domain.Aggregates.Device;
using MyTelegram.EventBus.RabbitMQ;
using MyTelegram.EventBus.RabbitMQ.Extensions;
using MyTelegram.Messenger;
using MyTelegram.Messenger.CommandServer.BackgroundServices;
using MyTelegram.Messenger.CommandServer.Extensions;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using MyTelegramConsts = MyTelegram.MyTelegramConsts;

Console.Title = $"MyTelegram messenger command server (layer {MyTelegramConsts.Layer})";

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.Console(theme: AnsiConsoleTheme.Code))
    .WriteTo.Async(c => c.File("Logs/startup-log.txt"))
    .CreateLogger();

Log.Information("{Info} {Version}", "MyTelegram messenger command server", typeof(Program).Assembly.GetName().Version);
Log.Information("{Description} {Url}",
    "For more information, please visit",
    MyTelegramConsts.RepositoryUrl);

Log.Information("MyTelegram messenger command server(API layer={Layer}) starting...",
    MyTelegramConsts.Layer);

AppDomain.CurrentDomain.UnhandledException += (_,
    e) =>
{
    Log.Error(e.ExceptionObject.ToString() ?? "UnhandledException");
};
TaskScheduler.UnobservedTaskException += (_,
    e) =>
{
    Log.Error(e.Exception.ToString());
};
var builder = Host.CreateDefaultBuilder(args);
//builder.UseAutofac();
builder.UseSerilog((context,
    configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.ConfigureHostOptions(options =>
{
    options.ServicesStartConcurrently = true;
    options.ServicesStopConcurrently = true;
});

builder.ConfigureAppConfiguration(options =>
{
    var configFile =
        Environment.GetEnvironmentVariable("MYTELEGRAM_CONFIG");
    if (!string.IsNullOrEmpty(configFile))
    {
        if (File.Exists(configFile))
        {
            options.AddJsonFile(configFile,
                false,
                true
            );
        }
    }

    options.AddEnvironmentVariables();
    options.AddCommandLine(args);
});
builder.ConfigureServices((context,
    services) =>
{
    services.AddOptions<MyTelegramMessengerServerOptions>()
        .Bind(context.Configuration.GetRequiredSection("App"))
        .ValidateDataAnnotations()
        .ValidateOnStart()
        ;
    var appConfig = context.Configuration.GetRequiredSection("App").Get<MyTelegramMessengerServerOptions>();

#if USE_REDIS_EVENTBUS
    services.AddMyTelegramRedisEventBus(context.Configuration);
#else
    services.AddMyTelegramRabbitMqEventBus(context.Configuration);
#endif
    

    services.AddMyTelegramMessengerCommandServer(options =>
    {
        options.AddDefaults(Assembly.GetEntryAssembly());
    });

    services.AddMyTelegramStackExchangeRedisCache(options =>
    {
        options.Configuration = context.Configuration.GetValue<string>("Redis:Configuration");
    });

    services.AddHostedService<MyTelegramCommandServerBackgroundService>();
    services.AddHostedService<MyTelegramInvokeAfterMsgProcessorBackgroundService>();

    services.AddHostedService<MessageQueueDataProcessorBackgroundService<MessengerCommandDataReceivedEvent>>();
    services.AddHostedService<MessageQueueDataProcessorBackgroundService<NewDeviceCreatedEvent>>();
    services.AddHostedService<MessageQueueDataProcessorBackgroundService<ISessionMessage>>();
    services.AddHostedService<QueuedCommandExecutorBackgroundService<DeviceAggregate, DeviceId>>();
    services.AddHostedService<QueuedCommandExecutorBackgroundService<PtsAggregate, PtsId>>();
    services.AddHostedService<QueuedCommandExecutorBackgroundService<ChannelAdminLogAggregate, ChannelAdminLogId>>();
    services.AddHostedService<ChannelViewsBackgroundService>();

    services.AddShardMessageQueueProcessor();
    services.Configure<HostOptions>(options =>
    {
        options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
    });
});


var app = builder.Build();

await app.RunAsync();
