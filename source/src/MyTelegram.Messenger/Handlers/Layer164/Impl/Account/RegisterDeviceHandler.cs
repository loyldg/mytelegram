// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Register device to receive <a href="https://corefork.telegram.org/api/push-updates">PUSH notifications</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 TOKEN_EMPTY The specified token is empty.
/// 400 TOKEN_INVALID The provided token is invalid.
/// 400 TOKEN_TYPE_INVALID The specified token type is invalid.
/// 400 WEBPUSH_AUTH_INVALID The specified web push authentication secret is invalid.
/// 400 WEBPUSH_KEY_INVALID The specified web push elliptic curve Diffie-Hellman public key is invalid.
/// 400 WEBPUSH_TOKEN_INVALID The specified web push token is invalid.
/// See <a href="https://corefork.telegram.org/method/account.registerDevice" />
///</summary>
internal sealed class RegisterDeviceHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestRegisterDevice, IBool>,
    Account.IRegisterDeviceHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestRegisterDevice obj)
    {
        throw new NotImplementedException();
    }
}
