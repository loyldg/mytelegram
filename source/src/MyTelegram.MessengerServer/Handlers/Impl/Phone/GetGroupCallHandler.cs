using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;
using IGroupCall = MyTelegram.Schema.Phone.IGroupCall;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class GetGroupCallHandler : RpcResultObjectHandler<RequestGetGroupCall, IGroupCall>,
    IGetGroupCallHandler
{
    protected override Task<IGroupCall> HandleCoreAsync(IRequestInput input,
        RequestGetGroupCall obj)
    {
        throw new NotImplementedException();
    }
}
