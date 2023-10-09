// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Send an email verification code.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 EMAIL_INVALID The specified email is invalid.
/// 400 EMAIL_NOT_SETUP In order to change the login email with emailVerifyPurposeLoginChange, an existing login email must already be set using emailVerifyPurposeLoginSetup.
/// 400 PHONE_HASH_EXPIRED An invalid or expired <code>phone_code_hash</code> was provided.
/// 400 PHONE_NUMBER_INVALID The phone number is invalid.
/// See <a href="https://corefork.telegram.org/method/account.sendVerifyEmailCode" />
///</summary>
internal sealed class SendVerifyEmailCodeHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSendVerifyEmailCode, MyTelegram.Schema.Account.ISentEmailCode>,
    Account.ISendVerifyEmailCodeHandler
{
    protected override Task<MyTelegram.Schema.Account.ISentEmailCode> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSendVerifyEmailCode obj)
    {
        throw new NotImplementedException();
    }
}
