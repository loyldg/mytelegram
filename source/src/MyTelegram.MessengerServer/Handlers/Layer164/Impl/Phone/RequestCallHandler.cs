using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;
using IPhoneCall = MyTelegram.Schema.Phone.IPhoneCall;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class RequestCallHandler : RpcResultObjectHandler<RequestRequestCall, IPhoneCall>,
    IRequestCallHandler, IProcessedHandler //, IShouldCacheRequest
{
    protected override Task<IPhoneCall> HandleCoreAsync(IRequestInput input,
        RequestRequestCall obj)
    {
        throw new NotImplementedException();
    }
}