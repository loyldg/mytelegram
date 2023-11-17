// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Confirm a phone number to cancel account deletion, for more info <a href="https://corefork.telegram.org/api/account-deletion">click here »</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CODE_HASH_INVALID Code hash invalid.
/// 400 PHONE_CODE_EMPTY phone_code is missing.
/// See <a href="https://corefork.telegram.org/method/account.confirmPhone" />
///</summary>
internal sealed class ConfirmPhoneHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestConfirmPhone, IBool>,
    Account.IConfirmPhoneHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestConfirmPhone obj)
    {
        throw new NotImplementedException();
    }
}
