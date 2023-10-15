using Microsoft.Extensions.Hosting;

namespace MyTelegram.Messenger.CommandServer.BackgroundServices;

public class DataProcessorBackgroundService : BackgroundService
{
    private readonly ILogger<DataProcessorBackgroundService> _logger;
    private readonly IMessageQueueProcessor<MessengerCommandDataReceivedEvent> _processor;

    public DataProcessorBackgroundService(IMessageQueueProcessor<MessengerCommandDataReceivedEvent> processor,
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
