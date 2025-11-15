namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Abort a pending 2FA password reset, <a href="https://corefork.telegram.org/api/srp#password-reset">see here for more info »</a>
/// <para><c>See <a href="https://corefork.telegram.org/method/account.declinePasswordReset"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DeclinePasswordResetHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestDeclinePasswordReset, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestDeclinePasswordReset obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}