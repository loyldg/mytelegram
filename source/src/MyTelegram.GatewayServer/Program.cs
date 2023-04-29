// See https://aka.ms/new-console-template for more information

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.Console(theme: AnsiConsoleTheme.Code))
    .WriteTo.Async(c => c.File("Logs/startup-log.txt"))
    .CreateLogger();

Log.Information("{Info} {Version}", "MyTelegram Gateway Server", typeof(Program).Assembly.GetName().Version);
Log.Information("{Description} {Url}", "For more information, please visit", "https://github.com/loyldg/mytelegram");

Log.Information("MyTelegram gateway server starting...");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context,
    configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddMyTelegramGatewayServer();
builder.Services.AddMyTelegramRabbitMqEventBus();
//services.AddSingleton<IEventBus, MyEventBusRabbitMq>();

builder.Services.Configure<EventBusRabbitMqOptions>(builder.Configuration.GetRequiredSection("RabbitMQ:EventBus"));
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetRequiredSection("RabbitMQ:Connections:Default"));
builder.Services.Configure<MyTelegramGatewayServerOption>(builder.Configuration.GetRequiredSection("App"));
var appConfig = builder.Configuration.GetRequiredSection("App").Get<MyTelegramGatewayServerOption>();

builder.Services.AddHostedService<EncryptedDataProcessorServerBackgroundService>();
builder.Services.AddHostedService<UnencryptedDataProcessorServerBackgroundService>();

builder.Services.AddTransient<WebsocketMiddleware>();

builder.WebHost.ConfigureKestrel(options =>
{
    if (appConfig != null)
    {
        foreach (var item in appConfig.Servers)
        {
            if (item.Enabled)
            {
                var ipAddress = string.IsNullOrEmpty(item.Ip)
                    ? item.Ipv6 ? IPAddress.IPv6Any : IPAddress.Any
                    : IPAddress.Parse(item.Ip);
                var iep = new IPEndPoint(ipAddress, item.Port);

                switch (item.ServerType)
                {
                    case ServerType.Tcp:
                        options.Listen(iep,
                            listenOptions =>
                            {
                                listenOptions.UseConnectionLogging()
                                    .UseConnectionHandler<MtpConnectionHandler>();
                            });

                        break;

                    case ServerType.Http:
                        options.Listen(iep,
                            listenOptions =>
                            {
                                if (item.Ssl)
                                {
                                    listenOptions.UseHttps(httpsOptions =>
                                    {
                                        var tlsCertificate = CertificateHelper.CreateX509Certificate(item);
                                        httpsOptions.ServerCertificate = tlsCertificate;
                                    });
                                }
                            });
                        break;
                }

                Log.Information("{ServerType} server started at:{Address},ssl:{Ssl}", item.ServerType, iep, item.Ssl);
            }
        }
    }
});
builder.Services.AddConnections();

var app = builder.Build();

app.UseWebSockets();
app.UseRouting();
app.UseMiddleware<WebsocketMiddleware>();

app.MapGet("/", () => "Only websocket requests are supported.");

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.ConfigureEventBus();

await app.RunAsync();
