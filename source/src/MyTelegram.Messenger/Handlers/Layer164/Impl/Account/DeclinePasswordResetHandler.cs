// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Abort a pending 2FA password reset, <a href="https://corefork.telegram.org/api/srp#password-reset">see here for more info »</a>
/// See <a href="https://corefork.telegram.org/method/account.declinePasswordReset" />
///</summary>
internal sealed class DeclinePasswordResetHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestDeclinePasswordReset, IBool>,
    Account.IDeclinePasswordResetHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestDeclinePasswordReset obj)
    {
        throw new NotImplementedException();
    }
}
