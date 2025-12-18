namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Send confirmation code to cancel account deletion, for more info <a href="https://corefork.telegram.org/api/account-deletion">click here »</a>
/// Possible errors
/// Code Type Description
/// 400 HASH_INVALID The provided hash is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.sendConfirmPhoneCode"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SendConfirmPhoneCodeHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSendConfirmPhoneCode, MyTelegram.Schema.Auth.ISentCode>
{
    protected override Task<MyTelegram.Schema.Auth.ISentCode> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestSendConfirmPhoneCode obj)
    {
        throw new NotImplementedException();
    }
}