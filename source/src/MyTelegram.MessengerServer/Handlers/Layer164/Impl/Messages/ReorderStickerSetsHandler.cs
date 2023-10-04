using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ReorderStickerSetsHandler : RpcResultObjectHandler<RequestReorderStickerSets, IBool>,
    IReorderStickerSetsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReorderStickerSets obj)
    {
        throw new NotImplementedException();
    }
}