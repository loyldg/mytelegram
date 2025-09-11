namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Install a theme
/// See <a href="https://corefork.telegram.org/method/account.installTheme" />
///</summary>
internal sealed class InstallThemeHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestInstallTheme, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestInstallTheme obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
