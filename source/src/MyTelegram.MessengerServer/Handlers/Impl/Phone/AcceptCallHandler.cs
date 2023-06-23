using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;
using IPhoneCall = MyTelegram.Schema.Phone.IPhoneCall;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class AcceptCallHandler : RpcResultObjectHandler<RequestAcceptCall, IPhoneCall>,
    IAcceptCallHandler
{
    protected override Task<IPhoneCall> HandleCoreAsync(IRequestInput input,
        RequestAcceptCall obj)
    {
        throw new NotImplementedException();
    }
}