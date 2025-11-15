namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Get saved <a href="https://corefork.telegram.org/passport">Telegram Passport</a> document, <a href="https://corefork.telegram.org/passport/encryption#encryption">for more info see the passport docs »</a>
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getSecureValue"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSecureValueHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetSecureValue, TVector<MyTelegram.Schema.ISecureValue>>
{
    protected override Task<TVector<MyTelegram.Schema.ISecureValue>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetSecureValue obj)
    {
        return Task.FromResult<TVector<MyTelegram.Schema.ISecureValue>>([]);
    }
}