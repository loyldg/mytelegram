using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class SendSignalingDataHandler : RpcResultObjectHandler<RequestSendSignalingData, IBool>,
    ISendSignalingDataHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSendSignalingData obj)
    {
        throw new NotImplementedException();
    }
}