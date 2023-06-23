using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class VerifyEmailHandler : RpcResultObjectHandler<RequestVerifyEmail, IEmailVerified>,
    IVerifyEmailHandler
{
    protected override Task<IEmailVerified> HandleCoreAsync(IRequestInput input,
        RequestVerifyEmail obj)
    {
        throw new NotImplementedException();
    }
}