using Microsoft.Extensions.Hosting;
using MyTelegram.Messenger.Extensions;
using MyTelegram.Messenger.Services.Filters;
using MyTelegram.Messenger.Services.Interfaces;
using MyTelegram.ReadModel.MongoDB;

namespace MyTelegram.Messenger.CommandServer.BackgroundServices;

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

    public MyTelegramMessengerServerInitBackgroundService(IServiceProvider serviceProvider,
        ILogger<MyTelegramMessengerServerInitBackgroundService> logger,
        IHandlerHelper handlerHelper,
        IDataSeeder dataSeeder,
        IIdGenerator idGenerator,
        IOptions<MyTelegramMessengerServerOptions> options,
        IMongoDbIndexesCreator mongoDbIndexesCreator)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _handlerHelper = handlerHelper;
        _dataSeeder = dataSeeder;
        _idGenerator = idGenerator;
        _mongoDbIndexesCreator = mongoDbIndexesCreator;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        _logger.LogInformation("App init starting...");
        _handlerHelper.InitAllHandlers(typeof(MyTelegramMessengerServerExtensions).Assembly);
        //IdGeneratorFactory.SetDefaultIdGenerator(_idGenerator);
        await _mongoDbIndexesCreator.CreateAllIndexesAsync();
        if (_options.UseInMemoryFilters)
        {
            await _serviceProvider.GetRequiredService<IInMemoryFilterDataLoader>().LoadAllFilterDataAsync()
         ;
        }
        await _dataSeeder.SeedAsync();
        _logger.LogInformation("Messenger service init ok");
    }
}
