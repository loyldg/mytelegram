// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get web <a href="https://corefork.telegram.org/widgets/login">login widget</a> authorizations
/// See <a href="https://corefork.telegram.org/method/account.getWebAuthorizations" />
///</summary>
internal sealed class GetWebAuthorizationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetWebAuthorizations, MyTelegram.Schema.Account.IWebAuthorizations>,
    Account.IGetWebAuthorizationsHandler
{
    protected override Task<MyTelegram.Schema.Account.IWebAuthorizations> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetWebAuthorizations obj)
    {
        throw new NotImplementedException();
    }
}
