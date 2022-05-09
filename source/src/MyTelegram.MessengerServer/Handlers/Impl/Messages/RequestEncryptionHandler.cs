using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class RequestEncryptionHandler : RpcResultObjectHandler<RequestRequestEncryption, IEncryptedChat>,
    IRequestEncryptionHandler
{
    protected override Task<IEncryptedChat> HandleCoreAsync(IRequestInput input,
        RequestRequestEncryption obj)
    {
        throw new NotImplementedException();
    }
}
