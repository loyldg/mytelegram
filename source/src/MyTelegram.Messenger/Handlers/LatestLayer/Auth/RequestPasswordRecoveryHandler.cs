namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;
/// <summary>
/// Request recovery code of a <a href="https://corefork.telegram.org/api/srp">2FA password</a>, only for accounts with a <a href="https://corefork.telegram.org/api/srp#email-verification">recovery email configured</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/auth.requestPasswordRecovery"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class RequestPasswordRecoveryHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestRequestPasswordRecovery, MyTelegram.Schema.Auth.IPasswordRecovery>
{
    protected override Task<MyTelegram.Schema.Auth.IPasswordRecovery> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestRequestPasswordRecovery obj)
    {
        throw new NotImplementedException();
    }
}