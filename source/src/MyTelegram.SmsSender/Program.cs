// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyTelegram.EventBus.Rebus;
using MyTelegram.Services.NativeAot;
using MyTelegram.SmsSender;
using Rebus.Config;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Net;

// Twilio new accounts and subaccounts are now required to use TLS 1.2 when accessing the REST API. 
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.Console(theme: AnsiConsoleTheme.Code))
    .WriteTo.Async(c => c.File("Logs/startup-log.txt"))
    .CreateLogger();

Log.Information("{Info} {Version}", "MyTelegram SMS Sender", typeof(Program).Assembly.GetName().Version);
Log.Information("{Description} {Url}", "For more information, please visit", "https://github.com/loyldg/mytelegram");

Log.Information("MyTelegram SMS sender starting...");

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureAppConfiguration(options => { options.AddEnvironmentVariables(); });

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

    services.AddRebusEventBus(options =>
    {
        var eventBusOptions = context.Configuration.GetRequiredSection("RabbitMQ:EventBus").Get<EventBusRabbitMqOptions>();
        var rabbitMqOptions = context.Configuration.GetRequiredSection("RabbitMQ:Connections:Default").Get<RabbitMqOptions>();

        options.Transport(t => t.UseRabbitMq($"amqp://{rabbitMqOptions.UserName}:{rabbitMqOptions.Password}@{rabbitMqOptions.HostName}:{rabbitMqOptions.Port}", eventBusOptions.ClientName));
        options.AddSystemTextJson(jsonOptions =>
        {
            jsonOptions.TypeInfoResolverChain.Add(MyJsonSerializeContext.Default);
        });
    });
});

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.ConfigureEventBus();

await app.RunAsync();
