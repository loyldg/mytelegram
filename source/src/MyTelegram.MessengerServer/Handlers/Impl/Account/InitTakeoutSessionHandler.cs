using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class InitTakeoutSessionHandler : RpcResultObjectHandler<RequestInitTakeoutSession, ITakeout>,
    IInitTakeoutSessionHandler
{
    protected override Task<ITakeout> HandleCoreAsync(IRequestInput input,
        RequestInitTakeoutSession obj)
    {
        throw new NotImplementedException();
    }
}
