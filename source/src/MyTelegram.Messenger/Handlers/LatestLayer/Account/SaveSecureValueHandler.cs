namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Securely save <a href="https://corefork.telegram.org/passport">Telegram Passport</a> document, <a href="https://corefork.telegram.org/passport/encryption#encryption">for more info see the passport docs »</a>
/// Possible errors
/// Code Type Description
/// 400 PASSWORD_REQUIRED A <a href="https://corefork.telegram.org/api/srp">2FA password</a> must be configured to use Telegram Passport.
/// 400 SECURE_SECRET_REQUIRED A secure secret is required.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.saveSecureValue"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SaveSecureValueHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSaveSecureValue, MyTelegram.Schema.ISecureValue>
{
    protected override Task<MyTelegram.Schema.ISecureValue> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestSaveSecureValue obj)
    {
        throw new NotImplementedException();
    }
}