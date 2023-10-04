using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class StartScheduledGroupCallHandler : RpcResultObjectHandler<RequestStartScheduledGroupCall, IUpdates>,
    IStartScheduledGroupCallHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestStartScheduledGroupCall obj)
    {
        throw new NotImplementedException();
    }
}