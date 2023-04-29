namespace MyTelegram.GatewayServer.BackgroundServices;

public class EncryptedDataProcessorServerBackgroundService : BackgroundService
{
    private readonly IMessageQueueProcessor<EncryptedMessage> _messageQueueProcessor;

    public EncryptedDataProcessorServerBackgroundService(IMessageQueueProcessor<EncryptedMessage> messageQueueProcessor)
    {
        _messageQueueProcessor = messageQueueProcessor;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return _messageQueueProcessor.ProcessAsync();
    }
}
