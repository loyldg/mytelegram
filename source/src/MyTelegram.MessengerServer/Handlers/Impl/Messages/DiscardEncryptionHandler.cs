using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class DiscardEncryptionHandler : RpcResultObjectHandler<RequestDiscardEncryption, IBool>,
    IDiscardEncryptionHandler, IProcessedHandler //, IShouldCacheRequest
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDiscardEncryption obj)
    {
        throw new NotImplementedException();
    }
}
