using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ClearRecentStickersHandler : RpcResultObjectHandler<RequestClearRecentStickers, IBool>,
    IClearRecentStickersHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestClearRecentStickers obj)
    {
        throw new NotImplementedException();
    }
}