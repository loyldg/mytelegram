namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Check whether the group call Server Forwarding Unit is currently receiving the streams with the specified WebRTC source IDs.<br/>
/// Returns an intersection of the source IDs specified in <code>sources</code>, and the source IDs currently being forwarded by the SFU.
/// Possible errors
/// Code Type Description
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// 400 GROUPCALL_JOIN_MISSING You haven't joined this group call.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.checkGroupCall"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class CheckGroupCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestCheckGroupCall, TVector<int>>
{
    protected override Task<TVector<int>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestCheckGroupCall obj)
    {
        throw new NotImplementedException();
    }
}