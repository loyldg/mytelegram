namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;
/// <summary>
/// Possible errors
/// Code Type Description
/// 500 AUTH_RESTART Restart the authorization process.
/// 400 CREDENTIAL_INVALID  
/// 500 PASSKEY_AUTH_RESTART  
/// <para><c>See <a href="https://corefork.telegram.org/method/auth.finishPasskeyLogin"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✔]
/// </remarks>
internal sealed class FinishPasskeyLoginHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestFinishPasskeyLogin, MyTelegram.Schema.Auth.IAuthorization>
{
    protected override async Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestFinishPasskeyLogin obj)
    {
        throw new NotImplementedException();
    }
}