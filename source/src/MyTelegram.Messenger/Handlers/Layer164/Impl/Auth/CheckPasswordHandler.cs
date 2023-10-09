// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Try logging to an account protected by a <a href="https://corefork.telegram.org/api/srp">2FA password</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PASSWORD_HASH_INVALID The provided password hash is invalid.
/// 400 SRP_ID_INVALID Invalid SRP ID provided.
/// 400 SRP_PASSWORD_CHANGED Password has changed.
/// See <a href="https://corefork.telegram.org/method/auth.checkPassword" />
///</summary>
internal sealed class CheckPasswordHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestCheckPassword, MyTelegram.Schema.Auth.IAuthorization>,
    Auth.ICheckPasswordHandler
{
    protected override Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestCheckPassword obj)
    {
        throw new NotImplementedException();
    }
}
