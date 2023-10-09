// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Request recovery code of a <a href="https://corefork.telegram.org/api/srp">2FA password</a>, only for accounts with a <a href="https://corefork.telegram.org/api/srp#email-verification">recovery email configured</a>.
/// See <a href="https://corefork.telegram.org/method/auth.requestPasswordRecovery" />
///</summary>
internal sealed class RequestPasswordRecoveryHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestRequestPasswordRecovery, MyTelegram.Schema.Auth.IPasswordRecovery>,
    Auth.IRequestPasswordRecoveryHandler
{
    protected override Task<MyTelegram.Schema.Auth.IPasswordRecovery> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestRequestPasswordRecovery obj)
    {
        throw new NotImplementedException();
    }
}
