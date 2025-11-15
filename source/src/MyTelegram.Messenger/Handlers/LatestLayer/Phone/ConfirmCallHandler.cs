namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// <a href="https://corefork.telegram.org/api/end-to-end/voice-calls">Complete phone call E2E encryption key exchange »</a>
/// Possible errors
/// Code Type Description
/// 400 CALL_ALREADY_DECLINED The call was already declined.
/// 400 CALL_PEER_INVALID The provided call peer object is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.confirmCall"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ConfirmCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestConfirmCall, MyTelegram.Schema.Phone.IPhoneCall>
{
    protected override Task<MyTelegram.Schema.Phone.IPhoneCall> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestConfirmCall obj)
    {
        throw new NotImplementedException();
    }
}