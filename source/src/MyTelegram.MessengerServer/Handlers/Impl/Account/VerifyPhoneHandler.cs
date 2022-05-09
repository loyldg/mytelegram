using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class VerifyPhoneHandler : RpcResultObjectHandler<RequestVerifyPhone, IBool>,
    IVerifyPhoneHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestVerifyPhone obj)
    {
        throw new NotImplementedException();
    }
}
