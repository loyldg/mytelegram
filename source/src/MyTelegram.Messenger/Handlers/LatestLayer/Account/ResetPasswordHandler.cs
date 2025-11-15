namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Initiate a 2FA password reset: can only be used if the user is already logged-in, <a href="https://corefork.telegram.org/api/srp#password-reset">see here for more info »</a>
/// <para><c>See <a href="https://corefork.telegram.org/method/account.resetPassword"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ResetPasswordHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestResetPassword, MyTelegram.Schema.Account.IResetPasswordResult>
{
    protected override Task<MyTelegram.Schema.Account.IResetPasswordResult> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestResetPassword obj)
    {
        throw new NotImplementedException();
    }
}