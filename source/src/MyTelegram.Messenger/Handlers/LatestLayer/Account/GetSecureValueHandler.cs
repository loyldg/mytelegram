namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Get saved <a href="https://corefork.telegram.org/passport">Telegram Passport</a> document, <a href="https://corefork.telegram.org/passport/encryption#encryption">for more info see the passport docs »</a>
/// See <a href="https://corefork.telegram.org/method/account.getSecureValue" />
///</summary>
internal sealed class GetSecureValueHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetSecureValue, TVector<MyTelegram.Schema.ISecureValue>>
{
    protected override Task<TVector<MyTelegram.Schema.ISecureValue>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetSecureValue obj)
    {
        return Task.FromResult<TVector<MyTelegram.Schema.ISecureValue>>([]);
    }
}
