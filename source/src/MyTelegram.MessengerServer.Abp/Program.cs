using System.Diagnostics.CodeAnalysis;
using MyTelegram;
using MyTelegram.MessengerServer;
using MyTelegram.MessengerServer.Abp;
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
builder.UseAutofac();
builder.UseSerilog((context,
    configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.ConfigureServices((context, services) =>
{
    services.Configure<MyTelegramMessengerServerOptions>(context.Configuration.GetRequiredSection("App"));
    services.AddApplication<MyTelegramMessengerServerAbpModule>();
});

await builder.Build().RunAsync();
