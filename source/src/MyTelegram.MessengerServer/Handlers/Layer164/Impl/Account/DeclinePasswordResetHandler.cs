using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class DeclinePasswordResetHandler : RpcResultObjectHandler<RequestDeclinePasswordReset, IBool>,
    IDeclinePasswordResetHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDeclinePasswordReset obj)
    {
        throw new NotImplementedException();
    }
}