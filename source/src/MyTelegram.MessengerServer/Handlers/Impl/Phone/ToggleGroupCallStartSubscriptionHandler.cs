using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class ToggleGroupCallStartSubscriptionHandler :
    RpcResultObjectHandler<RequestToggleGroupCallStartSubscription, IUpdates>,
    IToggleGroupCallStartSubscriptionHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestToggleGroupCallStartSubscription obj)
    {
        throw new NotImplementedException();
    }
}