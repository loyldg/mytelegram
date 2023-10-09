// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get all saved <a href="https://corefork.telegram.org/passport">Telegram Passport</a> documents, <a href="https://corefork.telegram.org/passport/encryption#encryption">for more info see the passport docs »</a>
/// See <a href="https://corefork.telegram.org/method/account.getAllSecureValues" />
///</summary>
internal sealed class GetAllSecureValuesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetAllSecureValues, TVector<MyTelegram.Schema.ISecureValue>>,
    Account.IGetAllSecureValuesHandler
{
    protected override Task<TVector<MyTelegram.Schema.ISecureValue>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetAllSecureValues obj)
    {
        throw new NotImplementedException();
    }
}
