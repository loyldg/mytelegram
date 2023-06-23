using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetAttachedStickersHandler :
    RpcResultObjectHandler<RequestGetAttachedStickers, TVector<IStickerSetCovered>>,
    IGetAttachedStickersHandler, IProcessedHandler
{
    protected override Task<TVector<IStickerSetCovered>> HandleCoreAsync(IRequestInput input,
        RequestGetAttachedStickers obj)
    {
        var r = new TVector<IStickerSetCovered>();
        return Task.FromResult(r);
    }
}