// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Reset all active web <a href="https://corefork.telegram.org/widgets/login">telegram login</a> sessions
/// See <a href="https://corefork.telegram.org/method/account.resetWebAuthorizations" />
///</summary>
internal sealed class ResetWebAuthorizationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestResetWebAuthorizations, IBool>,
    Account.IResetWebAuthorizationsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestResetWebAuthorizations obj)
    {
        throw new NotImplementedException();
    }
}
