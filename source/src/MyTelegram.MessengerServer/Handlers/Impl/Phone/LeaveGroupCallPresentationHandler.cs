using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class LeaveGroupCallPresentationHandler : RpcResultObjectHandler<RequestLeaveGroupCallPresentation, IUpdates>,
    ILeaveGroupCallPresentationHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestLeaveGroupCallPresentation obj)
    {
        throw new NotImplementedException();
    }
}
