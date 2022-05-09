using MyTelegram.MessengerServer.Abp;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

Console.Title = "MyTelegram messenger server";

Log.Logger = new LoggerConfiguration()
#if DEBUG
    //.MinimumLevel.Verbose()
    .MinimumLevel.Information()
#else
                .MinimumLevel.Information()
#endif
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("EventFlow", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Async(
        c =>
            c.Console(
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss:fffff} {Level:u3}] {Message:lj} {NewLine}{Exception}",
                theme: AnsiConsoleTheme.Code))
    //.WriteTo.Async(c => c.File("Logs/logs.txt"))
    .CreateLogger();
Log.Information("Messenger server starting...");

var builder = Host.CreateDefaultBuilder(args);
builder.UseAutofac();
builder.UseSerilog();

builder.ConfigureServices((_,
    services) => {
    services.AddApplication<MyTelegramMessengerServerAbpModule>();
});

await builder.Build().RunAsync();
