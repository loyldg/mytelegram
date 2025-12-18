namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Verify an email to use as <a href="https://corefork.telegram.org/api/srp">2FA recovery method</a>.
/// Possible errors
/// Code Type Description
/// 400 CODE_INVALID Code invalid.
/// 400 EMAIL_HASH_EXPIRED Email hash expired.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.confirmPasswordEmail"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ConfirmPasswordEmailHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestConfirmPasswordEmail, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestConfirmPasswordEmail obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}