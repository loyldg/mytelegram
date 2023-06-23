using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class JoinGroupCallHandler : RpcResultObjectHandler<RequestJoinGroupCall, IUpdates>,
    IJoinGroupCallHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestJoinGroupCall obj)
    {
        throw new NotImplementedException();
    }
}