namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Verify an email address.
/// Possible errors
/// Code Type Description
/// 400 CODE_INVALID Code invalid.
/// 400 EMAIL_INVALID The specified email is invalid.
/// 400 EMAIL_NOT_ALLOWED The specified email cannot be used to complete the operation.
/// 400 EMAIL_VERIFY_EXPIRED The verification email has expired.
/// 400 PHONE_CODE_EXPIRED The phone code you provided has expired.
/// 400 PHONE_NUMBER_INVALID The phone number is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.verifyEmail"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✔]
/// </remarks>
internal sealed class VerifyEmailHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestVerifyEmail, MyTelegram.Schema.Account.IEmailVerified>
{
    protected override Task<MyTelegram.Schema.Account.IEmailVerified> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestVerifyEmail obj)
    {
        throw new NotImplementedException();
    }
}