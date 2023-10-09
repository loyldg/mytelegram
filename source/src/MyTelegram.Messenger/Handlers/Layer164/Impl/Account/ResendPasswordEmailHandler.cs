// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Resend the code to verify an email to use as <a href="https://corefork.telegram.org/api/srp">2FA recovery method</a>.
/// See <a href="https://corefork.telegram.org/method/account.resendPasswordEmail" />
///</summary>
internal sealed class ResendPasswordEmailHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestResendPasswordEmail, IBool>,
    Account.IResendPasswordEmailHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestResendPasswordEmail obj)
    {
        throw new NotImplementedException();
    }
}
