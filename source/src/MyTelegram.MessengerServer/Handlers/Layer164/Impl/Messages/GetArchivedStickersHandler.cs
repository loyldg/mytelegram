using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetArchivedStickersHandler : RpcResultObjectHandler<RequestGetArchivedStickers, IArchivedStickers>,
    IGetArchivedStickersHandler, IProcessedHandler
{
    protected override Task<IArchivedStickers> HandleCoreAsync(IRequestInput input,
        RequestGetArchivedStickers obj)
    {
        var r = new TArchivedStickers { Sets = new TVector<IStickerSetCovered>() };

        return Task.FromResult<IArchivedStickers>(r);
    }
}