using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class InstallThemeHandler : RpcResultObjectHandler<RequestInstallTheme, IBool>,
    IInstallThemeHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestInstallTheme obj)
    {
        throw new NotImplementedException();
    }
}
