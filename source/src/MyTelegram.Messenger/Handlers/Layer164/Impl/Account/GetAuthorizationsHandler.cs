// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get logged-in sessions
/// See <a href="https://corefork.telegram.org/method/account.getAuthorizations" />
///</summary>
internal sealed class GetAuthorizationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetAuthorizations, MyTelegram.Schema.Account.IAuthorizations>,
    Account.IGetAuthorizationsHandler
{
    protected override Task<MyTelegram.Schema.Account.IAuthorizations> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetAuthorizations obj)
    {
        throw new NotImplementedException();
    }
}
