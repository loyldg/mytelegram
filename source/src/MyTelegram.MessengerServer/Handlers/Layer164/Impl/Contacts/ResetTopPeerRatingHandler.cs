using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class ResetTopPeerRatingHandler : RpcResultObjectHandler<RequestResetTopPeerRating, IBool>,
    IResetTopPeerRatingHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestResetTopPeerRating obj)
    {
        throw new NotImplementedException();
    }
}