using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ClearAllDraftsHandler : RpcResultObjectHandler<RequestClearAllDrafts, IBool>,
    IClearAllDraftsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestClearAllDrafts obj)
    {
        throw new NotImplementedException();
    }
}