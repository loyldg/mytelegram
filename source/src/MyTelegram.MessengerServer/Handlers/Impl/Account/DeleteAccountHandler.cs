using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class DeleteAccountHandler : RpcResultObjectHandler<RequestDeleteAccount, IBool>,
    IDeleteAccountHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDeleteAccount obj)
    {
        throw new NotImplementedException();
    }
}
