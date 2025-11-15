namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;
/// <summary>
/// Binds a temporary authorization key <code>temp_auth_key_id</code> to the permanent authorization key <code>perm_auth_key_id</code>. Each permanent key may only be bound to one temporary key at a time, binding a new temporary key overwrites the previous one.For more information, see <a href="https://corefork.telegram.org/api/pfs">Perfect Forward Secrecy</a>.
/// Possible errors
///   
/// nonce long Random long
/// temp_auth_key_id long Temporary auth_key_id
/// perm_auth_key_id long Permanent auth_key_id to bind to
/// temp_session_id long Session id, which will be used to invoke <strong>auth.bindTempAuthKey</strong> method
/// expires_at int Unix timestamp to invalidate temporary key
/// <para><c>See <a href="https://corefork.telegram.org/method/auth.bindTempAuthKey"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✔]
/// </remarks>
internal sealed class BindTempAuthKeyHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestBindTempAuthKey, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestBindTempAuthKey obj)
    {
        throw new NotImplementedException();
    }
}