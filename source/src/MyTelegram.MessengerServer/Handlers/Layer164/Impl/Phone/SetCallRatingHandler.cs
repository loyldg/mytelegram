using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class SetCallRatingHandler : RpcResultObjectHandler<RequestSetCallRating, IUpdates>,
    ISetCallRatingHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSetCallRating obj)
    {
        throw new NotImplementedException();
    }
}