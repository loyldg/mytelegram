using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class ReceivedCallHandler : RpcResultObjectHandler<RequestReceivedCall, IBool>,
    IReceivedCallHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReceivedCall obj)
    {
        throw new NotImplementedException();
    }
}