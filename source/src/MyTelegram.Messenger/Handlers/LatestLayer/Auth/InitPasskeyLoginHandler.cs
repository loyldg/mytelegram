namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;
/// <summary>
/// Possible errors
/// Code Type Description
/// 400 API_ID_INVALID API ID invalid.
/// 500 AUTH_RESTART Restart the authorization process.
/// 500 PASSKEY_AUTH_RESTART  
/// <para><c>See <a href="https://corefork.telegram.org/method/auth.initPasskeyLogin"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✔]
/// </remarks>
internal sealed class InitPasskeyLoginHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestInitPasskeyLogin, MyTelegram.Schema.Auth.IPasskeyLoginOptions>, IObjectHandler
{
    protected override async Task<MyTelegram.Schema.Auth.IPasskeyLoginOptions> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestInitPasskeyLogin obj)
    {
        throw new NotImplementedException();
    }
}