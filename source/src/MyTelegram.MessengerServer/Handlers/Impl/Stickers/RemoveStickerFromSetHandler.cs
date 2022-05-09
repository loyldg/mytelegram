using MyTelegram.Handlers.Stickers;
using MyTelegram.Schema.Stickers;
using IStickerSet = MyTelegram.Schema.Messages.IStickerSet;

namespace MyTelegram.MessengerServer.Handlers.Impl.Stickers;

public class RemoveStickerFromSetHandler : RpcResultObjectHandler<RequestRemoveStickerFromSet, IStickerSet>,
    IRemoveStickerFromSetHandler
{
    protected override Task<IStickerSet> HandleCoreAsync(IRequestInput input,
        RequestRemoveStickerFromSet obj)
    {
        throw new NotImplementedException();
    }
}
