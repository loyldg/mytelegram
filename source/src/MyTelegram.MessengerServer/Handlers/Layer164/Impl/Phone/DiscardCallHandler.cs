using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class DiscardCallHandler : RpcResultObjectHandler<RequestDiscardCall, IUpdates>,
    IDiscardCallHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestDiscardCall obj)
    {
        throw new NotImplementedException();
    }
}