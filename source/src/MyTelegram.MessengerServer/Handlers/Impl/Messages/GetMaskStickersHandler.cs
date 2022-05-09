using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;
using IStickerSet = MyTelegram.Schema.IStickerSet;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetMaskStickersHandler : RpcResultObjectHandler<RequestGetMaskStickers, IAllStickers>,
    IGetMaskStickersHandler, IProcessedHandler
{
    protected override Task<IAllStickers> HandleCoreAsync(IRequestInput input,
        RequestGetMaskStickers obj)
    {
        var r = new TAllStickers { Sets = new TVector<IStickerSet>(), Hash = obj.Hash };

        return Task.FromResult<IAllStickers>(r);
    }
}
