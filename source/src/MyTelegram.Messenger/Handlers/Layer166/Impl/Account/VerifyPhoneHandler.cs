// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Verify a phone number for telegram <a href="https://corefork.telegram.org/passport">passport</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PHONE_CODE_EMPTY phone_code is missing.
/// 400 PHONE_CODE_EXPIRED The phone code you provided has expired.
/// 400 PHONE_NUMBER_INVALID The phone number is invalid.
/// See <a href="https://corefork.telegram.org/method/account.verifyPhone" />
///</summary>
internal sealed class VerifyPhoneHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestVerifyPhone, IBool>,
    Account.IVerifyPhoneHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestVerifyPhone obj)
    {
        throw new NotImplementedException();
    }
}
