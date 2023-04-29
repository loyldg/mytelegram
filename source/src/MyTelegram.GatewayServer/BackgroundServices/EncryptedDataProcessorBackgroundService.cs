namespace MyTelegram.GatewayServer.BackgroundServices;

public class EncryptedDataProcessorBackgroundService : BackgroundService
{
    private readonly IMessageQueueProcessor<EncryptedMessage> _messageQueueProcessor;

    public EncryptedDataProcessorBackgroundService(IMessageQueueProcessor<EncryptedMessage> messageQueueProcessor)
    {
        _messageQueueProcessor = messageQueueProcessor;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return _messageQueueProcessor.ProcessAsync();
    }
}
