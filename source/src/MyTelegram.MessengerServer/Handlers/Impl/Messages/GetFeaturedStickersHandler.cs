using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetFeaturedStickersHandler : RpcResultObjectHandler<RequestGetFeaturedStickers, IFeaturedStickers>,
    IGetFeaturedStickersHandler, IProcessedHandler
{
    protected override Task<IFeaturedStickers> HandleCoreAsync(IRequestInput input,
        RequestGetFeaturedStickers obj)
    {
        return Task.FromResult<IFeaturedStickers>(new TFeaturedStickers
        {
            Sets = new TVector<IStickerSetCovered>(), Unread = new TVector<long>()
        });
    }
}
