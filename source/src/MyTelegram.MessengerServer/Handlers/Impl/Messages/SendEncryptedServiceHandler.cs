using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SendEncryptedServiceHandler : RpcResultObjectHandler<RequestSendEncryptedService, ISentEncryptedMessage>,
    ISendEncryptedServiceHandler
{
    protected override Task<ISentEncryptedMessage> HandleCoreAsync(IRequestInput input,
        RequestSendEncryptedService obj)
    {
        throw new NotImplementedException();
    }
}