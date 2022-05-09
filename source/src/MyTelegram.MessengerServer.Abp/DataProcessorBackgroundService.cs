namespace MyTelegram.MessengerServer.Abp;

public class DataProcessorBackgroundService : BackgroundService
{
    private readonly ILogger<DataProcessorBackgroundService> _logger;
    private readonly IMessageQueueProcessor<MessengerDataReceivedEvent> _processor;

    public DataProcessorBackgroundService(IMessageQueueProcessor<MessengerDataReceivedEvent> processor,
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
