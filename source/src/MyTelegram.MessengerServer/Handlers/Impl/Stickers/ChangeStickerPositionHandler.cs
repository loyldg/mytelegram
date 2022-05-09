using MyTelegram.Handlers.Stickers;
using MyTelegram.Schema.Stickers;
using IStickerSet = MyTelegram.Schema.Messages.IStickerSet;

namespace MyTelegram.MessengerServer.Handlers.Impl.Stickers;

public class ChangeStickerPositionHandler : RpcResultObjectHandler<RequestChangeStickerPosition, IStickerSet>,
    IChangeStickerPositionHandler
{
    protected override Task<IStickerSet> HandleCoreAsync(IRequestInput input,
        RequestChangeStickerPosition obj)
    {
        throw new NotImplementedException();
    }
}
