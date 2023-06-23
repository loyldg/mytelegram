using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class RequestPasswordRecoveryHandler : RpcResultObjectHandler<RequestRequestPasswordRecovery, IPasswordRecovery>,
    IRequestPasswordRecoveryHandler
{
    protected override Task<IPasswordRecovery> HandleCoreAsync(IRequestInput input,
        RequestRequestPasswordRecovery obj)
    {
        throw new NotImplementedException();
    }
}