// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Initiate a 2FA password reset: can only be used if the user is already logged-in, <a href="https://corefork.telegram.org/api/srp#password-reset">see here for more info »</a>
/// See <a href="https://corefork.telegram.org/method/account.resetPassword" />
///</summary>
internal sealed class ResetPasswordHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestResetPassword, MyTelegram.Schema.Account.IResetPasswordResult>,
    Account.IResetPasswordHandler
{
    protected override Task<MyTelegram.Schema.Account.IResetPasswordResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestResetPassword obj)
    {
        throw new NotImplementedException();
    }
}
