namespace MyTelegram.GatewayServer.BackgroundServices;

public class ClientDisconnectedDataProcessorBackgroundService : BackgroundService
{
    private readonly IMessageQueueProcessor<ClientDisconnectedEvent> _messageQueueProcessor;

    public ClientDisconnectedDataProcessorBackgroundService(IMessageQueueProcessor<ClientDisconnectedEvent> messageQueueProcessor)
    {
        _messageQueueProcessor = messageQueueProcessor;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return _messageQueueProcessor.ProcessAsync();
    }
}