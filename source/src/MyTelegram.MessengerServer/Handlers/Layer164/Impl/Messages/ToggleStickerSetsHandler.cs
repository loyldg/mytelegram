using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ToggleStickerSetsHandler : RpcResultObjectHandler<RequestToggleStickerSets, IBool>,
    IToggleStickerSetsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestToggleStickerSets obj)
    {
        throw new NotImplementedException();
    }
}