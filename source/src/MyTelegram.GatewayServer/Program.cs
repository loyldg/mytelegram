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
var configFile =
    Environment.GetEnvironmentVariable("MYTELEGRAM_CONFIG");
if (!string.IsNullOrEmpty(configFile))
{
    if (File.Exists(configFile))
    {
        builder.Configuration.AddJsonFile(configFile,
            false,
            true
        );
    }
}
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddCommandLine(args);

builder.Services.AddMyTelegramGatewayServer();
builder.Services.Configure<MyTelegramGatewayServerOption>(builder.Configuration.GetRequiredSection("App"));

#if USE_REDIS_EVENTBUS
builder.Services.AddMyTelegramRedisEventBus(builder.Configuration);
#else
builder.Services.AddMyTelegramRabbitMqEventBus(builder.Configuration);
#endif

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
                                //if (item.MediaOnly)
                                //{
                                //    listenOptions.Use(async (connectionContext, next) =>
                                //    {
                                //        connectionContext.Features.Set(new ConnectionTypeFeature(ConnectionType.Media, item.Id));
                                //        await next();
                                //    });
                                //}

                                listenOptions.Use(async (connectionContext, next) =>
                                {
                                    connectionContext.Features.Set(new ConnectionTypeFeature(item.MediaOnly ? ConnectionType.Media : ConnectionType.Generic, item.Id));
                                    await next();
                                });

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
                                //if (item.MediaOnly)
                                //{
                                //    listenOptions.Use(async (connectionContext, next) =>
                                //    {
                                //        connectionContext.Features.Set(new ConnectionTypeFeature(ConnectionType.Media, item.Id));
                                //        await next();
                                //    });
                                //}
                                listenOptions.Use(async (connectionContext, next) =>
                                    {
                                        connectionContext.Features.Set(new ConnectionTypeFeature(item.MediaOnly ? ConnectionType.Media : ConnectionType.Generic, item.Id));
                                        await next();
                                    });


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
if (appConfig?.UseForwardedHeaders ?? false)
{
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
}
app.UseWebSockets();
app.UseRouting();
app.UseMiddleware<WebSocketMiddleware>();
app.UseCors();

app.MapGet("/", () => "Only websocket requests are supported.");


await app.RunAsync();