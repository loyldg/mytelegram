using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;
using IPhoneCall = MyTelegram.Schema.Phone.IPhoneCall;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class ConfirmCallHandler : RpcResultObjectHandler<RequestConfirmCall, IPhoneCall>,
    IConfirmCallHandler
{
    protected override Task<IPhoneCall> HandleCoreAsync(IRequestInput input,
        RequestConfirmCall obj)
    {
        throw new NotImplementedException();
    }
}