// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Reset the <a href="https://corefork.telegram.org/api/srp">2FA password</a> using the recovery code sent using <a href="https://corefork.telegram.org/method/auth.requestPasswordRecovery">auth.requestPasswordRecovery</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CODE_EMPTY The provided code is empty.
/// 400 NEW_SETTINGS_INVALID The new password settings are invalid.
/// See <a href="https://corefork.telegram.org/method/auth.recoverPassword" />
///</summary>
internal sealed class RecoverPasswordHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestRecoverPassword, MyTelegram.Schema.Auth.IAuthorization>,
    Auth.IRecoverPasswordHandler
{
    protected override Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestRecoverPassword obj)
    {
        throw new NotImplementedException();
    }
}
