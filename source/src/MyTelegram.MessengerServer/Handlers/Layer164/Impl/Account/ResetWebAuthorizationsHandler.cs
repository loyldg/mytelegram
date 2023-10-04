using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class ResetWebAuthorizationsHandler : RpcResultObjectHandler<RequestResetWebAuthorizations, IBool>,
    IResetWebAuthorizationsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestResetWebAuthorizations obj)
    {
        throw new NotImplementedException();
    }
}