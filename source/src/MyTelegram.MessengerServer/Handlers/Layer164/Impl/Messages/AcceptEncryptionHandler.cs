using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class AcceptEncryptionHandler : RpcResultObjectHandler<RequestAcceptEncryption, IEncryptedChat>,
    IAcceptEncryptionHandler, IProcessedHandler //, IShouldCacheRequest
{
    protected override Task<IEncryptedChat> HandleCoreAsync(IRequestInput input,
        RequestAcceptEncryption obj)
    {
        throw new NotImplementedException();
    }
}