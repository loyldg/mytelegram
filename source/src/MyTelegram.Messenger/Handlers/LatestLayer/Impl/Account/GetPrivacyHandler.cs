// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get privacy settings of current account
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PRIVACY_KEY_INVALID The privacy key is invalid.
/// See <a href="https://corefork.telegram.org/method/account.getPrivacy" />
///</summary>
internal sealed class GetPrivacyHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetPrivacy, MyTelegram.Schema.Account.IPrivacyRules>,
    Account.IGetPrivacyHandler
{
    protected override Task<MyTelegram.Schema.Account.IPrivacyRules> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetPrivacy obj)
    {
        return Task.FromResult<IPrivacyRules>(new TPrivacyRules
        {
            Chats = new TVector<IChat>(),
            Rules = new TVector<IPrivacyRule>(),
            Users = new TVector<IUser>()
        });
    }
}
