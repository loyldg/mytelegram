namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;
/// <summary>
/// Try logging to an account protected by a <a href="https://corefork.telegram.org/api/srp">2FA password</a>.
/// Possible errors
/// Code Type Description
/// 500 AUTH_KEY_UNSYNCHRONIZED Internal error, please repeat the method call.
/// 400 PASSWORD_HASH_INVALID The provided password hash is invalid.
/// 400 SRP_ID_INVALID Invalid SRP ID provided.
/// 400 SRP_PASSWORD_CHANGED Password has changed.
/// <para><c>See <a href="https://corefork.telegram.org/method/auth.checkPassword"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✔]
/// </remarks>
internal sealed class CheckPasswordHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestCheckPassword, MyTelegram.Schema.Auth.IAuthorization>
{
    protected override Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestCheckPassword obj)
    {
        throw new NotImplementedException();
    }
}