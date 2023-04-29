namespace MyTelegram.GatewayServer.BackgroundServices;

public class UnencryptedDataProcessorServerBackgroundService : BackgroundService
{
    private readonly IMessageQueueProcessor<UnencryptedMessage> _messageQueueProcessor;

    public UnencryptedDataProcessorServerBackgroundService(IMessageQueueProcessor<UnencryptedMessage> messageQueueProcessor)
    {
        _messageQueueProcessor = messageQueueProcessor;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return _messageQueueProcessor.ProcessAsync();
    }
}