// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Securely save <a href="https://corefork.telegram.org/passport">Telegram Passport</a> document, <a href="https://corefork.telegram.org/passport/encryption#encryption">for more info see the passport docs »</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PASSWORD_REQUIRED A <a href="https://corefork.telegram.org/api/srp">2FA password</a> must be configured to use Telegram Passport.
/// See <a href="https://corefork.telegram.org/method/account.saveSecureValue" />
///</summary>
internal sealed class SaveSecureValueHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSaveSecureValue, MyTelegram.Schema.ISecureValue>,
    Account.ISaveSecureValueHandler
{
    protected override Task<MyTelegram.Schema.ISecureValue> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSaveSecureValue obj)
    {
        throw new NotImplementedException();
    }
}
