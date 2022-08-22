using EventFlow.MongoDB.Extensions;
using MyTelegram;
using MyTelegram.MessengerServer.GrpcService;
using MyTelegram.MessengerServer.GrpcService.Services;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

Console.Title = "MyTelegram messenger grpc service";

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.Console(theme: AnsiConsoleTheme.Code))
    .WriteTo.Async(c => c.File("Logs/startup-log.txt"))
    .CreateLogger();

Log.Information("{Info} {Version}", "MyTelegram Messenger GrpcService Server", typeof(Program).Assembly.GetName().Version);
Log.Information("{Description} {Url}", "For more information, please visit", MyTelegramServerDomainConsts.RepositoryUrl);

Log.Information("MyTelegram messenger server rpc service starting...");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context,
    configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});
builder.Configuration.AddEnvironmentVariables();

builder.Services.UseMyTelegramMessengerGrpcServer(options =>
{
    options.ConfigureMongoDb(builder.Configuration.GetConnectionString("Default"), builder.Configuration["App:DatabaseName"]);
});
builder.Services.AddGrpc();

var app = builder.Build();
app.MapGrpcService<ChatMemberLoaderGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
