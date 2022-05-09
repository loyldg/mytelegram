using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class ViewSponsoredMessageHandler : RpcResultObjectHandler<RequestViewSponsoredMessage, IBool>,
    IViewSponsoredMessageHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestViewSponsoredMessage obj)
    {
        throw new NotImplementedException();
    }
}
