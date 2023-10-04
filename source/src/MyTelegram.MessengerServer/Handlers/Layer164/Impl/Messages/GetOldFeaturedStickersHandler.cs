using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetOldFeaturedStickersHandler : RpcResultObjectHandler<RequestGetOldFeaturedStickers, IFeaturedStickers>,
    IGetOldFeaturedStickersHandler
{
    protected override Task<IFeaturedStickers> HandleCoreAsync(IRequestInput input,
        RequestGetOldFeaturedStickers obj)
    {
        throw new NotImplementedException();
    }
}