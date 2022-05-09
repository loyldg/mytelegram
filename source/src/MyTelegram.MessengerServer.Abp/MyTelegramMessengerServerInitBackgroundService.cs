namespace MyTelegram.MessengerServer.Abp;

public class MyTelegramMessengerServerInitBackgroundService : BackgroundService
{
    private readonly IDataSeeder _dataSeeder;
    private readonly IHandlerHelper _handlerHelper;
    private readonly ILogger<MyTelegramMessengerServerInitBackgroundService> _logger;
    private readonly IMongoDbIndexesCreator _mongoDbIndexesCreator;
    private readonly IInMemoryFilterDataLoader _filterDataLoader;

    public MyTelegramMessengerServerInitBackgroundService(
        ILogger<MyTelegramMessengerServerInitBackgroundService> logger,
        IHandlerHelper handlerHelper,
        IDataSeeder dataSeeder,
        IMongoDbIndexesCreator mongoDbIndexesCreator,
        IInMemoryFilterDataLoader filterDataLoader)
    {
        _logger = logger;
        _handlerHelper = handlerHelper;
        _dataSeeder = dataSeeder;
        _mongoDbIndexesCreator = mongoDbIndexesCreator;
        _filterDataLoader = filterDataLoader;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("App init starting...");
        _handlerHelper.InitAllHandlers(typeof(MyTelegramMessengerServerExtensions).Assembly);
        await _mongoDbIndexesCreator.CreateAllIndexesAsync().ConfigureAwait(false);

        await _dataSeeder.SeedAsync().ConfigureAwait(false);
        await _filterDataLoader.LoadAllFilterDataAsync().ConfigureAwait(false);
        _logger.LogInformation("Messenger service init ok");
    }
}
