using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SaveRecentStickerHandler : RpcResultObjectHandler<RequestSaveRecentSticker, IBool>,
    ISaveRecentStickerHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSaveRecentSticker obj)
    {
        throw new NotImplementedException();
    }
}
