using MyTelegram.Core;
using MyTelegram.MessengerServer;
using MyTelegram.MessengerServer.Extensions;
using MyTelegram.MessengerServer.GrpcService;
using MyTelegram.MessengerServer.GrpcService.Services;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

Console.Title = "MyTelegram messenger grpc service";

Log.Logger = new LoggerConfiguration()
#if DEBUG
    //.MinimumLevel.Verbose()
    .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Grpc", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Async(
        c =>
            c.Console(
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss:fffff} {Level:u3}] {Message:lj} {NewLine}{Exception}",
                theme: AnsiConsoleTheme.Code))
    //.WriteTo.Async(c => c.File("Logs/logs.txt"))
    .CreateLogger();
Log.Information("Messenger server rpc service starting...");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
builder.Services.AddMyTelegramCoreServices();
builder.Services.AddMyTelegramHandlerServices();
builder.Services.AddMyTelegramMessengerServices();
builder.Services.AddMyTelegramGrpcService();
builder.Services.AddMongoDbGrpcServiceEventFlow();
builder.Services.AddGrpc();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints => {
    endpoints.MapGrpcService<ChatMemberLoaderGrpcService>();
    endpoints.MapGrpcService<ChatGrpcService>();
    
    endpoints.MapGet("/", async context => {
        await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
    });
});

//app.MapGet("/", () => "Hello World!");

app.Run();
