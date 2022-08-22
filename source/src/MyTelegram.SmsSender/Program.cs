// See https://aka.ms/new-console-template for more information

using Serilog;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Sinks.SystemConsole.Themes;
using Microsoft.Extensions.Hosting;
using MyTelegram.SmsSender;

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
    services.AddHostedService<AbpInitializationHostedService>();
    // services.AddApplication<MyTelegramSmsSenderModule>();
});

await builder.Build().RunAsync();
