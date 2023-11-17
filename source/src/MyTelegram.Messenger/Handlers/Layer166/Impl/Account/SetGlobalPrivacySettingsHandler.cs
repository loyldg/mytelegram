// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Set global privacy settings
/// <para>Possible errors</para>
/// Code Type Description
/// 400 AUTOARCHIVE_NOT_AVAILABLE The autoarchive setting is not available at this time: please check the value of the <a href="https://corefork.telegram.org/api/config#client-configuration">autoarchive_setting_available field in client config »</a> before calling this method.
/// See <a href="https://corefork.telegram.org/method/account.setGlobalPrivacySettings" />
///</summary>
internal sealed class SetGlobalPrivacySettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSetGlobalPrivacySettings, MyTelegram.Schema.IGlobalPrivacySettings>,
    Account.ISetGlobalPrivacySettingsHandler
{
    protected override Task<MyTelegram.Schema.IGlobalPrivacySettings> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSetGlobalPrivacySettings obj)
    {
        throw new NotImplementedException();
    }
}
