using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class DiscardGroupCallHandler : RpcResultObjectHandler<RequestDiscardGroupCall, IUpdates>,
    IDiscardGroupCallHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestDiscardGroupCall obj)
    {
        throw new NotImplementedException();
    }
}
