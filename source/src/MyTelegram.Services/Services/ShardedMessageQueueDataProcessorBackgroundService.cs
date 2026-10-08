using Microsoft.Extensions.Hosting;

namespace MyTelegram.Services.Services;

public class ShardedMessageQueueDataProcessorBackgroundService<TData>(IShardedMessageQueueProcessor<TData> processor, ILogger<ShardedMessageQueueDataProcessorBackgroundService<TData>> logger) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("{TypeName} processor started", typeof(TData).Name);
        return processor.ProcessAsync(stoppingToken);
    }
}