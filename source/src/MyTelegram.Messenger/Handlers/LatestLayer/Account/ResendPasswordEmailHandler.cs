namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Resend the code to verify an email to use as <a href="https://corefork.telegram.org/api/srp">2FA recovery method</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.resendPasswordEmail"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ResendPasswordEmailHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestResendPasswordEmail, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestResendPasswordEmail obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}