using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;
using IStickerSet = MyTelegram.Schema.IStickerSet;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetAllStickersHandler : RpcResultObjectHandler<RequestGetAllStickers, IAllStickers>,
    IGetAllStickersHandler, IProcessedHandler
{
    protected override Task<IAllStickers> HandleCoreAsync(IRequestInput input,
        RequestGetAllStickers obj)
    {
        var r = new TAllStickers { Sets = new TVector<IStickerSet>() };

        return Task.FromResult<IAllStickers>(r);
    }
}
