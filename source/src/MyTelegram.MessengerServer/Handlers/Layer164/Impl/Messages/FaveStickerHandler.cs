using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class FaveStickerHandler : RpcResultObjectHandler<RequestFaveSticker, IBool>,
    IFaveStickerHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestFaveSticker obj)
    {
        throw new NotImplementedException();
    }
}