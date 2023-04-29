namespace MyTelegram.GatewayServer.BackgroundServices;

public class MessageDataProcessor : IDataProcessor<UnencryptedMessage>,
    IDataProcessor<EncryptedMessage>
{
    private readonly IEventBus _eventBus;

    public MessageDataProcessor(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public Task ProcessAsync(EncryptedMessage data)
    {
        return _eventBus.PublishAsync(data);
    }

    public Task ProcessAsync(UnencryptedMessage data)
    {
        return _eventBus.PublishAsync(data);
    }
}