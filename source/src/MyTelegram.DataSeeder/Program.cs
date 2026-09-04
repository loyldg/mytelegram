Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.Console(theme: AnsiConsoleTheme.Code))
    .WriteTo.Async(c => c.File("Logs/startup-log.txt"))
    .CreateLogger();

Log.Information("{Info} {Version}", "MyTelegram Data Seeder", typeof(Program).Assembly.GetName().Version);
Log.Information("{Description} {Url}", "For more information, please visit", "https://github.com/loyldg/mytelegram");

Log.Information("MyTelegram data seeder starting...");

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureAppConfiguration(options =>
{
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

    options.AddEnvironmentVariables();
    options.AddCommandLine(args);
});

builder.UseSerilog((context,
    configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.ConfigureHostOptions(options =>
{
    options.ServicesStartConcurrently = true;
    options.ServicesStopConcurrently = true;
    options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
});

builder.ConfigureServices((context,
    services) =>
{
    services.Configure<MyTelegramDataSeederOptions>(context.Configuration.GetRequiredSection("App"));
    // services.Configure<MinioOptions>(context.Configuration.GetRequiredSection("Minio"));

    services.AddMyTelegramDataSeeder();

    services.AddHostedService<MyTelegramDataSeederBackgroundService>();
});

var app = builder.Build();

await app.RunAsync();