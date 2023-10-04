using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class InviteToGroupCallHandler : RpcResultObjectHandler<RequestInviteToGroupCall, IUpdates>,
    IInviteToGroupCallHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestInviteToGroupCall obj)
    {
        throw new NotImplementedException();
    }
}