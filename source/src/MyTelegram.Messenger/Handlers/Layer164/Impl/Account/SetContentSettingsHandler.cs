// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Set sensitive content settings (for viewing or hiding NSFW content)
/// <para>Possible errors</para>
/// Code Type Description
/// 403 SENSITIVE_CHANGE_FORBIDDEN You can't change your sensitive content settings.
/// See <a href="https://corefork.telegram.org/method/account.setContentSettings" />
///</summary>
internal sealed class SetContentSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSetContentSettings, IBool>,
    Account.ISetContentSettingsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSetContentSettings obj)
    {
        throw new NotImplementedException();
    }
}
