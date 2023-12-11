// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Change the phone number of the current account
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PHONE_CODE_EMPTY phone_code is missing.
/// 400 PHONE_CODE_EXPIRED The phone code you provided has expired.
/// 406 PHONE_NUMBER_INVALID The phone number is invalid.
/// 400 PHONE_NUMBER_OCCUPIED The phone number is already in use.
/// See <a href="https://corefork.telegram.org/method/account.changePhone" />
///</summary>
internal sealed class ChangePhoneHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestChangePhone, MyTelegram.Schema.IUser>,
    Account.IChangePhoneHandler
{
    protected override Task<MyTelegram.Schema.IUser> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestChangePhone obj)
    {
        throw new NotImplementedException();
    }
}
