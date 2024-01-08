using Microsoft.Extensions.Options;

namespace MyTelegram.GatewayServer.BackgroundServices;

public class MessageDataProcessor : IDataProcessor<UnencryptedMessage>,
    IDataProcessor<EncryptedMessage>
{
    private readonly IEventBus _eventBus;
    private readonly IOptions<MyTelegramGatewayServerOption> _options;

    public MessageDataProcessor(IEventBus eventBus, IOptions<MyTelegramGatewayServerOption> options)
    {
        _eventBus = eventBus;
        _options = options;
    }

    public Task ProcessAsync(EncryptedMessage data)
    {
        return _eventBus.PublishAsync(new MyTelegram.Core.EncryptedMessage(data.AuthKeyId, data.MsgKey, data.EncryptedData, data.ConnectionId,
            _options.Value.MediaOnly ? ConnectionType.Media : ConnectionType.Generic,
            data.ClientIp, data.RequestId, data.Date));
    }

    public Task ProcessAsync(UnencryptedMessage data)
    {
        return _eventBus.PublishAsync(new MyTelegram.Core.UnencryptedMessage(data.AuthKeyId, data.ClientIp, data.ConnectionId, data.MessageData, data.MessageDataLength, data.MessageId, data.ObjectId, data.RequestId, data.Date));
    }
}
