using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class DeleteSecureValueHandler : RpcResultObjectHandler<RequestDeleteSecureValue, IBool>,
    IDeleteSecureValueHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDeleteSecureValue obj)
    {
        throw new NotImplementedException();
    }
}
