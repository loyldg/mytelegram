using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class UpdatePasswordSettingsHandler : RpcResultObjectHandler<RequestUpdatePasswordSettings, IBool>,
    IUpdatePasswordSettingsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUpdatePasswordSettings obj)
    {
        throw new NotImplementedException();
    }
}
