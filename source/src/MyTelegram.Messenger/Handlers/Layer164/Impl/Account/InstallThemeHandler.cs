// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Install a theme
/// See <a href="https://corefork.telegram.org/method/account.installTheme" />
///</summary>
internal sealed class InstallThemeHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestInstallTheme, IBool>,
    Account.IInstallThemeHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestInstallTheme obj)
    {
        throw new NotImplementedException();
    }
}
