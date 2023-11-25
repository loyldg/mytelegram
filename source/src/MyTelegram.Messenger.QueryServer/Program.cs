using EventFlow.MongoDB.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyTelegram;
using MyTelegram.Caching.Redis;
using MyTelegram.Messenger;
using MyTelegram.Messenger.QueryServer.BackgroundServices;
using MyTelegram.Messenger.QueryServer.Extensions;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

Console.Title = "MyTelegram messenger query server";

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
