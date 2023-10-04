using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class ResetPasswordHandler : RpcResultObjectHandler<RequestResetPassword, IResetPasswordResult>,
    IResetPasswordHandler
{
    protected override Task<IResetPasswordResult> HandleCoreAsync(IRequestInput input,
        RequestResetPassword obj)
    {
        throw new NotImplementedException();
    }
}