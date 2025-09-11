namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Verify an email to use as <a href="https://corefork.telegram.org/api/srp">2FA recovery method</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CODE_INVALID Code invalid.
/// 400 EMAIL_HASH_EXPIRED Email hash expired.
/// See <a href="https://corefork.telegram.org/method/account.confirmPasswordEmail" />
///</summary>
internal sealed class ConfirmPasswordEmailHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestConfirmPasswordEmail, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestConfirmPasswordEmail obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
