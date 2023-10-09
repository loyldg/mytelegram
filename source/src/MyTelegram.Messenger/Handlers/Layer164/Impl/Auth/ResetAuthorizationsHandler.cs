// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Terminates all user's authorized sessions except for the current one.After calling this method it is necessary to reregister the current device using the method <a href="https://corefork.telegram.org/method/account.registerDevice">account.registerDevice</a>
/// See <a href="https://corefork.telegram.org/method/auth.resetAuthorizations" />
///</summary>
internal sealed class ResetAuthorizationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestResetAuthorizations, IBool>,
    Auth.IResetAuthorizationsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestResetAuthorizations obj)
    {
        throw new NotImplementedException();
    }
}
