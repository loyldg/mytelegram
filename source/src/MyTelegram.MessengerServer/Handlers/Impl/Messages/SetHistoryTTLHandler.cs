using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SetHistoryTtlHandler : RpcResultObjectHandler<RequestSetHistoryTTL, IUpdates>,
    ISetHistoryTTLHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSetHistoryTTL obj)
    {
        throw new NotImplementedException();
    }
}
