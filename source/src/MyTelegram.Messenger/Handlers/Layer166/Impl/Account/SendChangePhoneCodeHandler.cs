// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Verify a new phone number to associate to the current account
/// <para>Possible errors</para>
/// Code Type Description
/// 406 FRESH_CHANGE_PHONE_FORBIDDEN You can't change phone number right after logging in, please wait at least 24 hours.
/// 400 PHONE_NUMBER_BANNED The provided phone number is banned from telegram.
/// 406 PHONE_NUMBER_INVALID The phone number is invalid.
/// 400 PHONE_NUMBER_OCCUPIED The phone number is already in use.
/// See <a href="https://corefork.telegram.org/method/account.sendChangePhoneCode" />
///</summary>
internal sealed class SendChangePhoneCodeHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSendChangePhoneCode, MyTelegram.Schema.Auth.ISentCode>,
    Account.ISendChangePhoneCodeHandler
{
    protected override Task<MyTelegram.Schema.Auth.ISentCode> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSendChangePhoneCode obj)
    {
        throw new NotImplementedException();
    }
}
