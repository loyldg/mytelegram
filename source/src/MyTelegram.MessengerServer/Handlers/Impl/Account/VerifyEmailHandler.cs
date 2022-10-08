using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class VerifyEmailHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestVerifyEmail, MyTelegram.Schema.Account.IEmailVerified>,
    IVerifyEmailHandler
{
    protected override Task<MyTelegram.Schema.Account.IEmailVerified> HandleCoreAsync(IRequestInput input,
        RequestVerifyEmail obj)
    {
        throw new NotImplementedException();
    }
}
