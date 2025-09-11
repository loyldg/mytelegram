// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyTelegram.EventBus.RabbitMQ.Extensions;
using MyTelegram.SmsSender;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.Console(theme: AnsiConsoleTheme.Code))
    .WriteTo.Async(c => c.File("Logs/startup-log.txt"))
    .CreateLogger();

Log.Information("{Info} {Version}", "MyTelegram SMS Sender", typeof(Program).Assembly.GetName().Version);
Log.Information("{Description} {Url}", "For more information, please visit", "https://github.com/loyldg/mytelegram");

Log.Information("MyTelegram SMS sender starting...");

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureAppConfiguration(options =>
{
    options.AddEnvironmentVariables();
    options.AddCommandLine(args);
});

builder.UseSerilog((context,
    configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.ConfigureServices((context,
    services) =>
{
    services.Configure<TwilioSmsOptions>(context.Configuration.GetRequiredSection("TwilioSms"));
    services.Configure<EventBusRabbitMqOptions>(context.Configuration.GetRequiredSection("RabbitMQ:EventBus"));
    services.Configure<RabbitMqOptions>(context.Configuration.GetRequiredSection("RabbitMQ:Connections:Default"));

    services.AddMyTelegramSmsSender();
    services.AddMyTelegramRabbitMqEventBus();
});

var app = builder.Build();


await app.RunAsync();
