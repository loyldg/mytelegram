// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get global privacy settings
/// See <a href="https://corefork.telegram.org/method/account.getGlobalPrivacySettings" />
///</summary>
internal sealed class GetGlobalPrivacySettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetGlobalPrivacySettings, MyTelegram.Schema.IGlobalPrivacySettings>,
    Account.IGetGlobalPrivacySettingsHandler
{
    protected override Task<MyTelegram.Schema.IGlobalPrivacySettings> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetGlobalPrivacySettings obj)
    {
        throw new NotImplementedException();
    }
}
