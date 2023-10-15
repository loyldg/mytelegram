using EventFlow.ReadStores;
using Microsoft.Extensions.Hosting;
using MyTelegram.Messenger.Extensions;
using MyTelegram.Messenger.Services.Filters;
using MyTelegram.Messenger.Services.Interfaces;
using MyTelegram.ReadModel.MongoDB;

namespace MyTelegram.Messenger.QueryServer.BackgroundServices;

//public class TestBackgroundService : BackgroundService
//{

//}

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
        //try
        //{
        //    var a = _serviceProvider
        //        .GetService<IMyMongoDbReadModelStore<MyTelegram.ReadModel.InMemory.UserReadModel>>();
        //    Console.WriteLine(a.GetType().FullName);
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine(ex);
        //}

        //var application = AbpApplicationFactory.Create<MyTelegramMessengerQueryServerModule>(options =>
        //{
        //    options.UseAutofac();
        //});
        //await application.InitializeAsync();
        var list = _readStoreManagers.ToList().OrderBy(p => p.ReadModelType.Name);
        foreach (var manager in list)
        {
            _logger.LogInformation("{Name}", manager.ReadModelType);
        }

        _logger.LogInformation("App init starting...");
        _handlerHelper.InitAllHandlers(typeof(MyTelegramMessengerServerExtensions).Assembly);
        //IdGeneratorFactory.SetDefaultIdGenerator(_idGenerator);
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
