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
        return _eventBus.PublishAsync(new MyTelegram.Core.EncryptedMessage(data.AuthKeyId, data.MsgKey, data.EncryptedData, data.ConnectionId, data.ClientIp, data.RequestId, data.Date));
    }

    public Task ProcessAsync(UnencryptedMessage data)
    {
        return _eventBus.PublishAsync(new MyTelegram.Core.UnencryptedMessage(data.AuthKeyId, data.ClientIp, data.ConnectionId, data.MessageData, data.MessageDataLength, data.MessageId, data.ObjectId, data.RequestId, data.Date));
    }
}
