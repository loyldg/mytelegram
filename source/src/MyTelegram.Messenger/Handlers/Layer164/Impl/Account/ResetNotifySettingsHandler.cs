// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Resets all notification settings from users and groups.
/// See <a href="https://corefork.telegram.org/method/account.resetNotifySettings" />
///</summary>
internal sealed class ResetNotifySettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestResetNotifySettings, IBool>,
    Account.IResetNotifySettingsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestResetNotifySettings obj)
    {
        throw new NotImplementedException();
    }
}
