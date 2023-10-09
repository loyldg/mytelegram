// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get days to live of account
/// See <a href="https://corefork.telegram.org/method/account.getAccountTTL" />
///</summary>
internal sealed class GetAccountTTLHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetAccountTTL, MyTelegram.Schema.IAccountDaysTTL>,
    Account.IGetAccountTTLHandler
{
    protected override Task<MyTelegram.Schema.IAccountDaysTTL> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetAccountTTL obj)
    {
        throw new NotImplementedException();
    }
}
