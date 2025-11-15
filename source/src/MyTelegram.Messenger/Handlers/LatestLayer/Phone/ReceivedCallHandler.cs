namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Optional: notify the server that the user is currently busy in a call: this will automatically refuse all incoming phone calls until the current phone call is ended.
/// Possible errors
/// Code Type Description
/// 400 CALL_ALREADY_DECLINED The call was already declined.
/// 400 CALL_PEER_INVALID The provided call peer object is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.receivedCall"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReceivedCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestReceivedCall, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestReceivedCall obj)
    {
        throw new NotImplementedException();
    }
}