// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Delete stored <a href="https://corefork.telegram.org/passport">Telegram Passport</a> documents, <a href="https://corefork.telegram.org/passport/encryption#encryption">for more info see the passport docs »</a>
/// See <a href="https://corefork.telegram.org/method/account.deleteSecureValue" />
///</summary>
internal sealed class DeleteSecureValueHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestDeleteSecureValue, IBool>,
    Account.IDeleteSecureValueHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestDeleteSecureValue obj)
    {
        throw new NotImplementedException();
    }
}
