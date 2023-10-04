using MyTelegram.Handlers.Stickers;
using MyTelegram.Schema.Stickers;
using IStickerSet = MyTelegram.Schema.Messages.IStickerSet;

namespace MyTelegram.MessengerServer.Handlers.Impl.Stickers;

public class AddStickerToSetHandler : RpcResultObjectHandler<RequestAddStickerToSet, IStickerSet>,
    IAddStickerToSetHandler
{
    protected override Task<IStickerSet> HandleCoreAsync(IRequestInput input,
        RequestAddStickerToSet obj)
    {
        throw new NotImplementedException();
    }
}