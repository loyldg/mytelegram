// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Verify an email address.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 EMAIL_INVALID The specified email is invalid.
/// 400 EMAIL_VERIFY_EXPIRED The verification email has expired.
/// See <a href="https://corefork.telegram.org/method/account.verifyEmail" />
///</summary>
internal sealed class VerifyEmailHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestVerifyEmail, MyTelegram.Schema.Account.IEmailVerified>,
    Account.IVerifyEmailHandler
{
    protected override Task<MyTelegram.Schema.Account.IEmailVerified> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestVerifyEmail obj)
    {
        throw new NotImplementedException();
    }
}
