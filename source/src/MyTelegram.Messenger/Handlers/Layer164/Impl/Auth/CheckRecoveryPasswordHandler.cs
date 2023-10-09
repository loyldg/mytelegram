// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Check if the <a href="https://corefork.telegram.org/api/srp">2FA recovery code</a> sent using <a href="https://corefork.telegram.org/method/auth.requestPasswordRecovery">auth.requestPasswordRecovery</a> is valid, before passing it to <a href="https://corefork.telegram.org/method/auth.recoverPassword">auth.recoverPassword</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PASSWORD_RECOVERY_EXPIRED The recovery code has expired.
/// See <a href="https://corefork.telegram.org/method/auth.checkRecoveryPassword" />
///</summary>
internal sealed class CheckRecoveryPasswordHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestCheckRecoveryPassword, IBool>,
    Auth.ICheckRecoveryPasswordHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestCheckRecoveryPassword obj)
    {
        throw new NotImplementedException();
    }
}
