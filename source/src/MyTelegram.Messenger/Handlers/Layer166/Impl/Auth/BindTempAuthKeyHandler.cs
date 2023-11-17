// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Binds a temporary authorization key <code>temp_auth_key_id</code> to the permanent authorization key <code>perm_auth_key_id</code>. Each permanent key may only be bound to one temporary key at a time, binding a new temporary key overwrites the previous one.For more information, see <a href="https://corefork.telegram.org/api/pfs">Perfect Forward Secrecy</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ENCRYPTED_MESSAGE_INVALID Encrypted message invalid.
/// 400 TEMP_AUTH_KEY_ALREADY_BOUND The passed temporary key is already bound to another <strong>perm_auth_key_id</strong>.
/// 400 TEMP_AUTH_KEY_EMPTY No temporary auth key provided.
/// See <a href="https://corefork.telegram.org/method/auth.bindTempAuthKey" />
///</summary>
internal sealed class BindTempAuthKeyHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestBindTempAuthKey, IBool>,
    Auth.IBindTempAuthKeyHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestBindTempAuthKey obj)
    {
        throw new NotImplementedException();
    }
}
