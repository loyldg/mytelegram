using MyTelegram.Handlers.Stickers;
using MyTelegram.Schema.Stickers;
using IStickerSet = MyTelegram.Schema.Messages.IStickerSet;

namespace MyTelegram.MessengerServer.Handlers.Impl.Stickers;

public class SetStickerSetThumbHandler : RpcResultObjectHandler<RequestSetStickerSetThumb, IStickerSet>,
    ISetStickerSetThumbHandler
{
    protected override Task<IStickerSet> HandleCoreAsync(IRequestInput input,
        RequestSetStickerSetThumb obj)
    {
        throw new NotImplementedException();
    }
}
