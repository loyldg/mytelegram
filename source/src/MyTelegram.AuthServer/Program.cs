Console.Title = "MyTelegram auth server";
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.Console(theme: AnsiConsoleTheme.Code))
    .WriteTo.Async(c => c.File("Logs/startup-log.txt"))
    .CreateLogger();

Log.Information(
    "{Info} {Version}",
    "MyTelegram Auth Server",
    typeof(Program).Assembly.GetName().Version
);
Log.Information(
    "{Description} {Url}",
    "For more information, please visit",
    MyTelegramConsts.RepositoryUrl
);

Log.Information("MyTelegram authentication server starting...");

//Console.ReadLine();
var builder = Host.CreateDefaultBuilder(args);



builder.ConfigureAppConfiguration(options =>
{
    options.AddEnvironmentVariables();
    options.AddCommandLine(args);

    var configFile =
        Environment.GetEnvironmentVariable("MYTELEGRAM_CONFIG");
    if (!string.IsNullOrEmpty(configFile))
    {
        if (File.Exists(configFile))
        {
            options.AddJsonFile(configFile,
                false,
                true
            );
        }
    }
});

builder.UseSerilog(
    (context, configuration) => { configuration.ReadFrom.Configuration(context.Configuration); }
);
builder.ConfigureServices(
    (context, services) =>
    {
        services.Configure<MyTelegramAuthServerOptions>(
            context.Configuration.GetRequiredSection("App")
        );

        services.AddHostedService<MyTelegramAuthServerBackgroundService>();
        services.AddAuthServer();
        services.AddMyTelegramStackExchangeRedisCache(options =>
        {
            options.Configuration = context.Configuration.GetValue<string>("Redis:Configuration");
        });
        services.AddCacheJsonSerializer(options =>
        {
            options.TypeInfoResolverChain.Add(MyJsonSerializeContext.Default);
        });
#if USE_REDIS_EVENTBUS
        services.AddMyTelegramRedisEventBus(context.Configuration);
#else
        services.AddMyTelegramRabbitMqEventBus(context.Configuration);
#endif
    }
);

var app = builder.Build();


await app.RunAsync();