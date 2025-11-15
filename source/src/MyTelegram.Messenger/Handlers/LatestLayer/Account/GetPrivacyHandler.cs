namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Get privacy settings of current account
/// Possible errors
/// Code Type Description
/// 400 PRIVACY_KEY_INVALID The privacy key is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getPrivacy"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetPrivacyHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetPrivacy, MyTelegram.Schema.Account.IPrivacyRules>
{
    protected override Task<MyTelegram.Schema.Account.IPrivacyRules> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetPrivacy obj)
    {
        return Task.FromResult<IPrivacyRules>(new TPrivacyRules { Chats = new TVector<IChat>(), Rules = new TVector<IPrivacyRule>(), Users = new TVector<IUser>() });
    }
}