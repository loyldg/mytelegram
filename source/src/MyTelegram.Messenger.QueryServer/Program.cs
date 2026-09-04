using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyTelegram;
using MyTelegram.Caching.Redis;
using MyTelegram.EventBus.RabbitMQ.Extensions;
using MyTelegram.Messenger;
using MyTelegram.Messenger.QueryServer.BackgroundServices;
using MyTelegram.Messenger.QueryServer.Extensions;
using MyTelegram.Services.NativeAot;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using MyTelegramConsts = MyTelegram.MyTelegramConsts;

Console.Title = $"MyTelegram messenger query server (layer {MyTelegramConsts.Layer})";

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.Console(theme: AnsiConsoleTheme.Code))
    .WriteTo.Async(c => c.File("Logs/startup-log.txt"))
    .CreateLogger();

Log.Information("{Info} {Version}", "MyTelegram messenger query server", typeof(Program).Assembly.GetName().Version);
Log.Information("{Description} {Url}",
    "For more information, please visit",
    MyTelegramConsts.RepositoryUrl);

Log.Information("MyTelegram messenger query server(API layer={Layer}) starting...",
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
builder.ConfigureServices((ctx,
    services) =>
{
    services.AddOptions<MyTelegramMessengerServerOptions>()
        .Bind(ctx.Configuration.GetRequiredSection("App"))
        .ValidateDataAnnotations()
        .ValidateOnStart()
        ;

    services.Configure<EventBusRabbitMqOptions>(ctx.Configuration.GetRequiredSection("RabbitMQ:EventBus"));
    services.Configure<RabbitMqOptions>(ctx.Configuration.GetRequiredSection("RabbitMQ:Connections:Default"));

    var eventBusOptions = ctx.Configuration.GetRequiredSection("RabbitMQ:EventBus").Get<EventBusRabbitMqOptions>();
    var rabbitMqOptions = ctx.Configuration.GetRequiredSection("RabbitMQ:Connections:Default").Get<RabbitMqOptions>();

    services.AddMyTelegramRabbitMqEventBus();
    //services.AddRebusEventBus(options =>
    //{
    //    options.Transport(t =>
    //    {
    //        t.UseRabbitMq(
    //                $"amqp://{rabbitMqOptions!.UserName}:{rabbitMqOptions.Password}@{rabbitMqOptions.HostName}:{rabbitMqOptions.Port}",
    //                eventBusOptions!.ClientName)
    //            .ExchangeNames(eventBusOptions.ExchangeName, eventBusOptions.TopicExchangeName ?? "RebusTopics")
    //            ;
    //    });
    //    options.AddSystemTextJson(jsonOptions =>
    //    {
    //        jsonOptions.TypeInfoResolverChain.Add(MyJsonSerializeContext.Default);
    //    });
    //});

    services.AddMyTelegramMessengerQueryServer();

    services.AddMyTelegramStackExchangeRedisCache(options =>
    {
        options.Configuration = ctx.Configuration.GetValue<string>("Redis:Configuration");
    });

    services.AddHostedService<MessageQueueDataProcessorBackgroundService<IDomainEvent>>();
    services.AddHostedService<MyTelegramQueryServerBackgroundService>();
    services.AddHostedService<DataProcessorBackgroundService>();
    services.AddHostedService<ObjectMessageSenderBackgroundService>();
    services.AddHostedService<MyTelegramInvokeAfterMsgProcessorBackgroundService>();
    services.AddHostedService<QueuedCommandExecutorBackgroundService>();

    services.Configure<HostOptions>(options =>
    {
        options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
    });
});

var app = builder.Build();

var handlerHelper = app.Services.GetRequiredService<IHandlerHelper>();
handlerHelper.InitAllHandlers();

await app.RunAsync();
