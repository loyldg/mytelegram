namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Broadcast a blockchain block to all members of a conference call, see <a href="https://corefork.telegram.org/api/end-to-end/group-calls">here »</a> for more info.
/// Possible errors
/// Code Type Description
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.sendConferenceCallBroadcast"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SendConferenceCallBroadcastHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestSendConferenceCallBroadcast, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestSendConferenceCallBroadcast obj)
    {
        throw new NotImplementedException();
    }
}