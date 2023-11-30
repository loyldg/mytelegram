using EventFlow.MongoDB.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyTelegram;
using MyTelegram.Caching.Redis;
using MyTelegram.Messenger;
using MyTelegram.Messenger.CommandServer.BackgroundServices;
using MyTelegram.Messenger.CommandServer.Extensions;
using MyTelegram.Messenger.Extensions;
using MyTelegram.Services.NativeAot;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

Console.Title = "MyTelegram messenger command server";

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.Console(theme: AnsiConsoleTheme.Code))
    .WriteTo.Async(c => c.File("Logs/startup-log.txt"))
    .CreateLogger();

Log.Information("{Info} {Version}", "MyTelegram messenger command server", typeof(Program).Assembly.GetName().Version);
Log.Information("{Description} {Url}",
    "For more information, please visit",
    MyTelegramServerDomainConsts.RepositoryUrl);

Log.Information("MyTelegram messenger command server(supported layer={Layer}) starting...",
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

builder.ConfigureAppConfiguration(options => { options.AddEnvironmentVariables(); });
builder.ConfigureServices((ctx,
    services) =>
{
    services.Configure<MyTelegramMessengerServerOptions>(ctx.Configuration.GetRequiredSection("App"));
    services.Configure<EventBusRabbitMqOptions>(ctx.Configuration.GetRequiredSection("RabbitMQ:EventBus"));
    services.Configure<RabbitMqOptions>(ctx.Configuration.GetRequiredSection("RabbitMQ:Connections:Default"));

    //services.AddMyTelegramRabbitMqEventBus();

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

    services.AddMyTelegramMessengerServer(options =>
    {
        options.AddDefaults(Assembly.GetEntryAssembly());
        options.ConfigureMongoDb(ctx.Configuration.GetConnectionString("Default"),
            ctx.Configuration["App:DatabaseName"]);
    });

    services.AddMyTelegramStackExchangeRedisCache(options =>
    {
        options.Configuration = ctx.Configuration.GetValue<string>("Redis:Configuration");
    });
    services.AddMyTelegramMessengerCommandServer();

    services.AddHostedService<MyTelegramMessengerServerInitBackgroundService>();
    services.AddHostedService<DataProcessorBackgroundService>();
    services.AddHostedService<ObjectMessageSenderBackgroundService>();
    services.AddHostedService<MyTelegramInvokeAfterMsgProcessorBackgroundService>();

    services.Configure<HostOptions>(options =>
    {
        options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
    });
});


var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.ConfigureEventBus();

await app.RunAsync();
