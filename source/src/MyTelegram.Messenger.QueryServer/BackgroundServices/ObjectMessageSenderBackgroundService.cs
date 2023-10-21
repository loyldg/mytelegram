using Microsoft.Extensions.Hosting;

namespace MyTelegram.Messenger.QueryServer.BackgroundServices;

public class ObjectMessageSenderBackgroundService : BackgroundService
{
    private readonly ILogger<ObjectMessageSenderBackgroundService> _logger;
    private readonly IMessageQueueProcessor<ISessionMessage> _processor;

    public ObjectMessageSenderBackgroundService(IMessageQueueProcessor<ISessionMessage> processor,
        ILogger<ObjectMessageSenderBackgroundService> logger)
    {
        _processor = processor;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Object message sender service started");
        return _processor.ProcessAsync();
    }
}
