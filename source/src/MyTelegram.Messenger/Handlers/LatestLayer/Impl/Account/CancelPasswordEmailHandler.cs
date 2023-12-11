// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Cancel the code that was sent to verify an email to use as <a href="https://corefork.telegram.org/api/srp">2FA recovery method</a>.
/// See <a href="https://corefork.telegram.org/method/account.cancelPasswordEmail" />
///</summary>
internal sealed class CancelPasswordEmailHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestCancelPasswordEmail, IBool>,
    Account.ICancelPasswordEmailHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestCancelPasswordEmail obj)
    {
        throw new NotImplementedException();
    }
}
