using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class FinishTakeoutSessionHandler : RpcResultObjectHandler<RequestFinishTakeoutSession, IBool>,
    IFinishTakeoutSessionHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestFinishTakeoutSession obj)
    {
        throw new NotImplementedException();
    }
}