using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class ResendPasswordEmailHandler : RpcResultObjectHandler<RequestResendPasswordEmail, IBool>,
    IResendPasswordEmailHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestResendPasswordEmail obj)
    {
        throw new NotImplementedException();
    }
}