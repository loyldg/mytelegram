namespace MyTelegram.GatewayServer.BackgroundServices;

public class UnencryptedDataProcessorBackgroundService : BackgroundService
{
    private readonly IMessageQueueProcessor<UnencryptedMessage> _messageQueueProcessor;

    public UnencryptedDataProcessorBackgroundService(
        IMessageQueueProcessor<UnencryptedMessage> messageQueueProcessor)
    {
        _messageQueueProcessor = messageQueueProcessor;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return _messageQueueProcessor.ProcessAsync();
    }
}