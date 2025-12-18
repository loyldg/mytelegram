namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Refuse or end running call
/// Possible errors
/// Code Type Description
/// 400 CALL_ALREADY_ACCEPTED The call was already accepted.
/// 500 CALL_OCCUPY_FAILED The call failed because the user is already making another call.
/// 400 CALL_PEER_INVALID The provided call peer object is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.discardCall"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DiscardCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestDiscardCall, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestDiscardCall obj)
    {
        throw new NotImplementedException();
    }
}