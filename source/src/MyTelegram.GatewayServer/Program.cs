// See https://aka.ms/new-console-template for more information

using MyTelegram.EventBus.RabbitMQ.Extensions;

Console.Title = "MyTelegram gateway server";

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
builder.Configuration.AddCommandLine(args);

builder.Services.AddMyTelegramGatewayServer();

builder.Services.Configure<EventBusRabbitMqOptions>(builder.Configuration.GetRequiredSection("RabbitMQ:EventBus"));
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetRequiredSection("RabbitMQ:Connections:Default"));
builder.Services.Configure<MyTelegramGatewayServerOption>(builder.Configuration.GetRequiredSection("App"));

var eventBusOptions = builder.Configuration.GetRequiredSection("RabbitMQ:EventBus").Get<EventBusRabbitMqOptions>();
var rabbitMqOptions = builder.Configuration.GetRequiredSection("RabbitMQ:Connections:Default").Get<RabbitMqOptions>();
if (rabbitMqOptions == null)
{
    Log.Error("RabbitMQ:Connections:Default is null");
    return;
}

if (eventBusOptions == null)
{
    Log.Error("RabbitMQ:EventBus is null");

    return;
}

builder.Services.AddMyTelegramRabbitMqEventBus();
//builder.Services.AddRebusEventBus(options =>
//{
//    options.Transport(t =>
//    {
//        t.UseRabbitMq(
//                $"amqp://{rabbitMqOptions.UserName}:{rabbitMqOptions.Password}@{rabbitMqOptions.HostName}:{rabbitMqOptions.Port}",
//                eventBusOptions.ClientName)
//            .ExchangeNames(eventBusOptions.ExchangeName, eventBusOptions.TopicExchangeName ?? "RebusTopics")
//            ;
//    });

//    options.AddSystemTextJson(jsonOptions =>
//    {
//        jsonOptions.TypeInfoResolverChain.Add(GatewayServerJsonContext.Default);
//    });
//});

var appConfig = builder.Configuration.GetRequiredSection("App").Get<MyTelegramGatewayServerOption>();

if (appConfig == null)
{
    Log.Error("Get app config failed.");
}

builder.Services.AddHostedService<EncryptedDataProcessorBackgroundService>();
builder.Services.AddHostedService<UnencryptedDataProcessorBackgroundService>();
builder.Services.AddHostedService<ClientDisconnectedDataProcessorBackgroundService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(p =>
    {
        p.AllowAnyOrigin();
        p.AllowAnyHeader();
        p.AllowAnyMethod();
    });
});

builder.Services.AddTransient<WebSocketMiddleware>();

builder.WebHost.ConfigureKestrel(options =>
{
    if (appConfig != null)
    {
        var logger = options.ApplicationServices.GetRequiredService<ILogger<ProxyProtocol>>();
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
                                if (item.MediaOnly)
                                {
                                    listenOptions.Use(async (connectionContext, next) =>
                                    {
                                        connectionContext.Features.Set(new ConnectionTypeFeature(ConnectionType.Media));
                                        await next();
                                    });
                                }

                                if (item.EnableProxyProtocolV2)
                                {
                                    listenOptions.Use(async (connectionContext, next) =>
                                    {
                                        await ProxyProtocol.ProcessAsync(connectionContext, next, logger);
                                    });
                                }

                                listenOptions
                                    //.UseConnectionLogging()
                                    .UseConnectionHandler<MtpConnectionHandler>()
                                    ;
                            });

                        break;

                    case ServerType.Http:
                        options.Listen(iep,
                            listenOptions =>
                            {
                                if (item.MediaOnly)
                                {
                                    listenOptions.Use(async (connectionContext, next) =>
                                    {
                                        connectionContext.Features.Set(new ConnectionTypeFeature(ConnectionType.Media));
                                        await next();
                                    });
                                }

                                if (item.EnableProxyProtocolV2)
                                {
                                    //listenOptions.UseConnectionHandler<ProxyProtocolHandler>();
                                    listenOptions.Use(async (connectionContext, next) =>
                                    {
                                        await ProxyProtocol.ProcessAsync(connectionContext, next, logger);
                                    });
                                }

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

                Log.Information(
                    "{ServerType} server started at:{Address},ssl:{Ssl},enableProxyProtocolV2:{ProxyProtocol},mediaOnly:{MediaOnly}",
                    item.ServerType, iep, item.Ssl, item.EnableProxyProtocolV2, item.MediaOnly);
            }
        }
    }
});
builder.Services.AddConnections();

var app = builder.Build();

app.UseWebSockets();
app.UseRouting();
app.UseMiddleware<WebSocketMiddleware>();
app.UseCors();

app.MapGet("/", () => "Only websocket requests are supported.");


await app.RunAsync();