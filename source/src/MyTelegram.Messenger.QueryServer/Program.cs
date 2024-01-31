using EventFlow.MongoDB.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyTelegram;
using MyTelegram.Caching.Redis;
using MyTelegram.Messenger;
using MyTelegram.Messenger.NativeAot;
using MyTelegram.Messenger.QueryServer.BackgroundServices;
using MyTelegram.Messenger.QueryServer.Extensions;
using MyTelegram.Schema.Extensions;
using MyTelegram.Services.NativeAot;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

Console.Title = "MyTelegram messenger query server";
var entities = new TVector<IMessageEntity>
{
    new TMessageEntityBold { Offset = 0, Length = 11 },
    new TMessageEntitySpoiler{Offset = 11,Length = 5},
    new TMessageEntityBold { Offset = 22, Length = 3 }
};
var updates = new TUpdateShortMessage
{
    Out = true,
    Id = 3001,
    UserId = 2000001,
    Message = "Login code: 22222. Do not give this code to anyone, even if they say they are from Telegram!\n\nThis code can be used to log in to your Telegram account. We never ask it for anything else.\n\nIf you didn\u0027t request this code by trying to log in on another device, simply ignore this message.",
    Pts = 3001,
    PtsCount = 1,
    Date = 1706607593,
    FwdFrom = null,
    ReplyTo = null,
    Entities = entities
};

Console.WriteLine("b5");
Console.WriteLine(BitConverter.ToString(updates.ToBytes()));

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.Console(theme: AnsiConsoleTheme.Code))
    .WriteTo.Async(c => c.File("Logs/startup-log.txt"))
    .CreateLogger();

Log.Information("{Info} {Version}", "MyTelegram messenger query server", typeof(Program).Assembly.GetName().Version);
Log.Information("{Description} {Url}",
    "For more information, please visit",
    MyTelegramServerDomainConsts.RepositoryUrl);

Log.Information("MyTelegram messenger query server(supported layer={Layer}) starting...",
    MyTelegramServerDomainConsts.Layer);

AppDomain.CurrentDomain.UnhandledException += (s,
    e) =>
{
    Log.Error(e.ExceptionObject.ToString());
};
TaskScheduler.UnobservedTaskException += (s,
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

builder.ConfigureAppConfiguration(options => { options.AddEnvironmentVariables(); });
builder.ConfigureServices((ctx,
    services) =>
{
    services.Configure<MyTelegramMessengerServerOptions>(ctx.Configuration.GetRequiredSection("App"));
    services.Configure<EventBusRabbitMqOptions>(ctx.Configuration.GetRequiredSection("RabbitMQ:EventBus"));
    services.Configure<RabbitMqOptions>(ctx.Configuration.GetRequiredSection("RabbitMQ:Connections:Default"));

    var eventBusOptions = ctx.Configuration.GetRequiredSection("RabbitMQ:EventBus").Get<EventBusRabbitMqOptions>();
    var rabbitMqOptions = ctx.Configuration.GetRequiredSection("RabbitMQ:Connections:Default").Get<RabbitMqOptions>();

    services.AddRebusEventBus(options =>
    {
        options.Transport(t => t.UseRabbitMq($"amqp://{rabbitMqOptions.UserName}:{rabbitMqOptions.Password}@{rabbitMqOptions.HostName}:{rabbitMqOptions.Port}", eventBusOptions.ClientName));
        options.AddSystemTextJson(jsonOptions =>
        {
            jsonOptions.TypeInfoResolverChain.Add(MyJsonSerializeContext.Default);
        });
    });

    services.AddMyTelegramMessengerQueryServer(options =>
    {
        ////options.AddDefaults(Assembly.GetEntryAssembly());
        options.ConfigureMongoDb(ctx.Configuration.GetConnectionString("Default"),
            ctx.Configuration["App:QueryServerEventStoreDatabaseName"]);

    });

    services.AddMyTelegramStackExchangeRedisCache(options =>
    {
        options.Configuration = ctx.Configuration.GetValue<string>("Redis:Configuration");
    });
    services.AddCacheJsonSerializer(options =>
    {
        options.TypeInfoResolverChain.Add(MyJsonSerializeContext.Default);
        options.TypeInfoResolverChain.Add(MyMessengerJsonContext.Default);
    });

    services.AddHostedService<MyTelegramMessengerServerInitBackgroundService>();
    services.AddHostedService<DataProcessorBackgroundService>();
    services.AddHostedService<ObjectMessageSenderBackgroundService>();
    services.AddHostedService<MyTelegramInvokeAfterMsgProcessorBackgroundService>();
    services.AddHostedService<CommandExecutorBackgroundService>();

    services.Configure<HostOptions>(options =>
    {
        options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
    });
});

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.ConfigureEventBus();

await app.RunAsync();
