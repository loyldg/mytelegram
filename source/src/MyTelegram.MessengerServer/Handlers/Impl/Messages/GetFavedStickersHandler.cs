using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetFavedStickersHandler : RpcResultObjectHandler<RequestGetFavedStickers, IFavedStickers>,
    IGetFavedStickersHandler, IProcessedHandler
{
    protected override Task<IFavedStickers> HandleCoreAsync(IRequestInput input,
        RequestGetFavedStickers obj)
    {
        return Task.FromResult<IFavedStickers>(new TFavedStickers {
            Packs = new TVector<IStickerPack>(), Stickers = new TVector<IDocument>()
        });
    }
}
