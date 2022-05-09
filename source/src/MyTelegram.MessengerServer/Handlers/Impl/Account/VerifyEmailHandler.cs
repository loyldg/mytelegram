using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class VerifyEmailHandler : RpcResultObjectHandler<RequestVerifyEmail, IBool>,
    IVerifyEmailHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestVerifyEmail obj)
    {
        throw new NotImplementedException();
    }
}
