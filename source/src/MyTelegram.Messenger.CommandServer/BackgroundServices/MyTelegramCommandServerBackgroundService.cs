using Microsoft.Extensions.Hosting;
using MyTelegram.EventFlow.MongoDB;
using MyTelegram.Messenger.Services.Caching;

namespace MyTelegram.Messenger.CommandServer.BackgroundServices;

public class MyTelegramCommandServerBackgroundService(
    ILogger<MyTelegramCommandServerBackgroundService> logger,
    IHandlerHelper handlerHelper,
    IMongoDbIndexesCreator mongoDbIndexesCreator,
    IInMemoryCacheLoader inMemoryCacheLoader)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Command server starting...");
        await mongoDbIndexesCreator.CreateAllIndexesAsync();
        handlerHelper.InitAllHandlers();
        await inMemoryCacheLoader.LoadAsync();

        logger.LogInformation("Command server started");
    }
}
