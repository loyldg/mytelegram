namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;
/// <summary>
/// Reset the <a href="https://corefork.telegram.org/api/srp">2FA password</a> using the recovery code sent using <a href="https://corefork.telegram.org/method/auth.requestPasswordRecovery">auth.requestPasswordRecovery</a>.
/// Possible errors
/// Code Type Description
/// 400 CODE_EMPTY The provided code is empty.
/// 400 NEW_SETTINGS_INVALID The new password settings are invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/auth.recoverPassword"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class RecoverPasswordHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestRecoverPassword, MyTelegram.Schema.Auth.IAuthorization>
{
    protected override Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestRecoverPassword obj)
    {
        throw new NotImplementedException();
    }
}