namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;
/// <summary>
/// Check if the <a href="https://corefork.telegram.org/api/srp">2FA recovery code</a> sent using <a href="https://corefork.telegram.org/method/auth.requestPasswordRecovery">auth.requestPasswordRecovery</a> is valid, before passing it to <a href="https://corefork.telegram.org/method/auth.recoverPassword">auth.recoverPassword</a>.
/// Possible errors
/// Code Type Description
/// 400 CODE_EMPTY The provided code is empty.
/// 400 PASSWORD_RECOVERY_EXPIRED The recovery code has expired.
/// <para><c>See <a href="https://corefork.telegram.org/method/auth.checkRecoveryPassword"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class CheckRecoveryPasswordHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestCheckRecoveryPassword, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestCheckRecoveryPassword obj)
    {
        throw new NotImplementedException();
    }
}