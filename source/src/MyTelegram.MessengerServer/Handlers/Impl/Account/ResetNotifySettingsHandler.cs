using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class ResetNotifySettingsHandler : RpcResultObjectHandler<RequestResetNotifySettings, IBool>,
    IResetNotifySettingsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestResetNotifySettings obj)
    {
        throw new NotImplementedException();
    }
}