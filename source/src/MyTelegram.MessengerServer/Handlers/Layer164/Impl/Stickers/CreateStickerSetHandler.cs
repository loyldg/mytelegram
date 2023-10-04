using MyTelegram.Handlers.Stickers;
using MyTelegram.Schema.Stickers;
using IStickerSet = MyTelegram.Schema.Messages.IStickerSet;

namespace MyTelegram.MessengerServer.Handlers.Impl.Stickers;

public class CreateStickerSetHandler : RpcResultObjectHandler<RequestCreateStickerSet, IStickerSet>,
    ICreateStickerSetHandler
{
    protected override Task<IStickerSet> HandleCoreAsync(IRequestInput input,
        RequestCreateStickerSet obj)
    {
        throw new NotImplementedException();
    }
}