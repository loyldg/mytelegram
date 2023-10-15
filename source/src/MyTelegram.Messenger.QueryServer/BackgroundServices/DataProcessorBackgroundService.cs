using Microsoft.Extensions.Hosting;

namespace MyTelegram.Messenger.QueryServer.BackgroundServices;

public class DataProcessorBackgroundService : BackgroundService
{
    private readonly ILogger<DataProcessorBackgroundService> _logger;
    private readonly IMessageQueueProcessor<MessengerQueryDataReceivedEvent> _processor;

    public DataProcessorBackgroundService(IMessageQueueProcessor<MessengerQueryDataReceivedEvent> processor,
        ILogger<DataProcessorBackgroundService> logger)
    {
        _processor = processor;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Data processor started");
        return _processor.ProcessAsync();
    }
}
