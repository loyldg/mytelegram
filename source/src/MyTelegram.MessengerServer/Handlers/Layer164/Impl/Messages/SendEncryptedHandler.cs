using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SendEncryptedHandler : RpcResultObjectHandler<RequestSendEncrypted, ISentEncryptedMessage>,
    ISendEncryptedHandler
{
    protected override Task<ISentEncryptedMessage> HandleCoreAsync(IRequestInput input,
        RequestSendEncrypted obj)
    {
        throw new NotImplementedException();
    }
}