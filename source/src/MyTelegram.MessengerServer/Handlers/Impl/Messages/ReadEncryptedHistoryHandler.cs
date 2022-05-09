using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ReadEncryptedHistoryHandler : RpcResultObjectHandler<RequestReadEncryptedHistory, IBool>,
    IReadEncryptedHistoryHandler, IProcessedHandler //, IShouldCacheRequest
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReadEncryptedHistory obj)
    {
        throw new NotImplementedException();
    }
}
