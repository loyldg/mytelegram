using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetRecentStickersHandler : RpcResultObjectHandler<RequestGetRecentStickers, IRecentStickers>,
    IGetRecentStickersHandler, IProcessedHandler
{
    protected override Task<IRecentStickers> HandleCoreAsync(IRequestInput input,
        RequestGetRecentStickers obj)
    {
        var r = new TRecentStickers {
            Packs = new TVector<IStickerPack>(), Stickers = new TVector<IDocument>(), Dates = new TVector<int>()
        };

        return Task.FromResult<IRecentStickers>(r);
    }
}
