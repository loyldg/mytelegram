using EventFlow.ReadStores;
using Microsoft.Extensions.Hosting;
using MyTelegram.Messenger.Services.Filters;
using MyTelegram.Messenger.Services.Interfaces;

namespace MyTelegram.Messenger.QueryServer.BackgroundServices;

public class MyTelegramMessengerServerInitBackgroundService : BackgroundService
{
    private readonly IDataSeeder _dataSeeder;
    private readonly IHandlerHelper _handlerHelper;
    private readonly IIdGenerator _idGenerator;
    private readonly ILogger<MyTelegramMessengerServerInitBackgroundService> _logger;
    private readonly IMongoDbIndexesCreator _mongoDbIndexesCreator;
    private readonly MyTelegramMessengerServerOptions _options;
    private readonly IServiceProvider _serviceProvider;
    //private readonly IReadOnlyCollection<IReadStoreManager> _readStoreManagers;
    private readonly IEnumerable<IReadStoreManager> _readStoreManagers;
    //private readonly IMyMongoDbReadModelStore<MyTelegram.ReadModel.InMemory.UserReadModel> _readModelStore;

    public MyTelegramMessengerServerInitBackgroundService(IServiceProvider serviceProvider,
        ILogger<MyTelegramMessengerServerInitBackgroundService> logger,
        IHandlerHelper handlerHelper,
        IDataSeeder dataSeeder,
        IIdGenerator idGenerator,
        IOptions<MyTelegramMessengerServerOptions> options,
        IMongoDbIndexesCreator mongoDbIndexesCreator, IEnumerable<IReadStoreManager> readStoreManagers
        /*IReadOnlyCollection<IReadStoreManager> readStoreManagers*/)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _handlerHelper = handlerHelper;
        _dataSeeder = dataSeeder;
        _idGenerator = idGenerator;
        _mongoDbIndexesCreator = mongoDbIndexesCreator;
        _readStoreManagers = readStoreManagers;
        //_readModelStore = readModelStore;
        //_readStoreManagers = readStoreManagers;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("App init starting...");
        _handlerHelper.InitAllHandlers(typeof(MyTelegramMessengerServerExtensions).Assembly);
        await _mongoDbIndexesCreator.CreateAllIndexesAsync();
        if (_options.UseInMemoryFilters)
        {
            await _serviceProvider.GetRequiredService<IInMemoryFilterDataLoader>().LoadAllFilterDataAsync()
         ;
        }
        //await _dataSeeder.SeedAsync();
        _logger.LogInformation("Messenger service init ok");
    }
}
