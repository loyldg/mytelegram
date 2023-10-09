// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Resend the login code via another medium, the phone code type is determined by the return value of the previous auth.sendCode/auth.resendCode: see <a href="https://corefork.telegram.org/api/auth">login</a> for more info.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PHONE_CODE_EMPTY phone_code is missing.
/// 400 PHONE_CODE_EXPIRED The phone code you provided has expired.
/// 400 PHONE_CODE_HASH_EMPTY phone_code_hash is missing.
/// 406 PHONE_NUMBER_INVALID The phone number is invalid.
/// 406 SEND_CODE_UNAVAILABLE Returned when all available options for this type of number were already used (e.g. flash-call, then SMS, then this error might be returned to trigger a second resend).
/// See <a href="https://corefork.telegram.org/method/auth.resendCode" />
///</summary>
internal sealed class ResendCodeHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestResendCode, MyTelegram.Schema.Auth.ISentCode>,
    Auth.IResendCodeHandler
{
    protected override Task<MyTelegram.Schema.Auth.ISentCode> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestResendCode obj)
    {
        throw new NotImplementedException();
    }
}
