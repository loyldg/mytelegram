// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Send confirmation code to cancel account deletion, for more info <a href="https://corefork.telegram.org/api/account-deletion">click here »</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 HASH_INVALID The provided hash is invalid.
/// See <a href="https://corefork.telegram.org/method/account.sendConfirmPhoneCode" />
///</summary>
internal sealed class SendConfirmPhoneCodeHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSendConfirmPhoneCode, MyTelegram.Schema.Auth.ISentCode>,
    Account.ISendConfirmPhoneCodeHandler
{
    protected override Task<MyTelegram.Schema.Auth.ISentCode> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSendConfirmPhoneCode obj)
    {
        throw new NotImplementedException();
    }
}
