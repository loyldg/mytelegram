using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class ResetWebAuthorizationHandler : RpcResultObjectHandler<RequestResetWebAuthorization, IBool>,
    IResetWebAuthorizationHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestResetWebAuthorization obj)
    {
        throw new NotImplementedException();
    }
}
