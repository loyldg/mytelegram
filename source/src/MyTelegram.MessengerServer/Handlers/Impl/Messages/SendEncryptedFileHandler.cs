using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SendEncryptedFileHandler : RpcResultObjectHandler<RequestSendEncryptedFile, ISentEncryptedMessage>,
    ISendEncryptedFileHandler
{
    protected override Task<ISentEncryptedMessage> HandleCoreAsync(IRequestInput input,
        RequestSendEncryptedFile obj)
    {
        throw new NotImplementedException();
    }
}
