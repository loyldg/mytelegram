using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class CreateGroupCallHandler : RpcResultObjectHandler<RequestCreateGroupCall, IUpdates>,
    ICreateGroupCallHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestCreateGroupCall obj)
    {
        throw new NotImplementedException();
    }
}
