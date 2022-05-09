using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class ConfirmPhoneHandler : RpcResultObjectHandler<RequestConfirmPhone, IBool>,
    IConfirmPhoneHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestConfirmPhone obj)
    {
        throw new NotImplementedException();
    }
}
