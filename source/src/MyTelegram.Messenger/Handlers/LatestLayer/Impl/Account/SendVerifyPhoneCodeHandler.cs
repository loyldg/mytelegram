// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Send the verification phone code for telegram <a href="https://corefork.telegram.org/passport">passport</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PHONE_NUMBER_INVALID The phone number is invalid.
/// See <a href="https://corefork.telegram.org/method/account.sendVerifyPhoneCode" />
///</summary>
internal sealed class SendVerifyPhoneCodeHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSendVerifyPhoneCode, MyTelegram.Schema.Auth.ISentCode>,
    Account.ISendVerifyPhoneCodeHandler
{
    protected override Task<MyTelegram.Schema.Auth.ISentCode> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSendVerifyPhoneCode obj)
    {
        throw new NotImplementedException();
    }
}
