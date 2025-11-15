namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Cancel the code that was sent to verify an email to use as <a href="https://corefork.telegram.org/api/srp">2FA recovery method</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.cancelPasswordEmail"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class CancelPasswordEmailHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestCancelPasswordEmail, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestCancelPasswordEmail obj)
    {
        throw new NotImplementedException();
    }
}