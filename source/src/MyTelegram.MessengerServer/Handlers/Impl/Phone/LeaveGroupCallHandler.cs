using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class LeaveGroupCallHandler : RpcResultObjectHandler<RequestLeaveGroupCall, IUpdates>,
    ILeaveGroupCallHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestLeaveGroupCall obj)
    {
        throw new NotImplementedException();
    }
}