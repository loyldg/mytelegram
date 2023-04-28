using System.Diagnostics.CodeAnalysis;
using EventFlow.MongoDB.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyTelegram;
using MyTelegram.Caching.Redis;
using MyTelegram.EventBus.RabbitMQ;
using MyTelegram.MessengerServer;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

Console.Title = $"MyTelegram messenger server,Layer={MyTelegramServerDomainConsts.Layer}";

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.Console(theme: AnsiConsoleTheme.Code))
    .WriteTo.Async(c => c.File("Logs/startup-log.txt"))
    .CreateLogger();

Log.Information("{Info} {Version}", "MyTelegram Messenger Server", typeof(Program).Assembly.GetName().Version);
Log.Information("{Description} {Url}", "For more information, please visit", MyTelegramServerDomainConsts.RepositoryUrl);

Log.Information("Messenger server(supported layer={Layer}) starting...", MyTelegramServerDomainConsts.Layer);

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureAppConfiguration(options =>
{
    options.AddEnvironmentVariables();
});
builder.UseSerilog((context,
    configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.ConfigureServices((context, services) =>
{
    services.Configure<MyTelegramMessengerServerOptions>(context.Configuration.GetRequiredSection("App"));
    services.Configure<EventBusRabbitMqOptions>(context.Configuration.GetRequiredSection("RabbitMQ:EventBus"));
    services.Configure<RabbitMqOptions>(context.Configuration.GetRequiredSection("RabbitMQ:Connections:Default"));

    services.AddMyTelegramStackExchangeRedisCache(options =>
    {
        options.Configuration = context.Configuration.GetValue<string>("Redis:Configuration");
    });

    services.AddMyTelegramMessengerServer(options =>
    {
        options.ConfigureMongoDb(context.Configuration.GetConnectionString("Default"),
            context.Configuration["App:DatabaseName"]
        );
    });
});

var app = builder.Build();
var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.ConfigureEventBus();

await app.RunAsync();

