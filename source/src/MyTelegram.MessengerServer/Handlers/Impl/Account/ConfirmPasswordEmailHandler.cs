using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class ConfirmPasswordEmailHandler : RpcResultObjectHandler<RequestConfirmPasswordEmail, IBool>,
    IConfirmPasswordEmailHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestConfirmPasswordEmail obj)
    {
        throw new NotImplementedException();
    }
}
