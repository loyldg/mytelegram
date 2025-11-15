namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Leave a group call
/// Possible errors
/// Code Type Description
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.leaveGroupCall"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class LeaveGroupCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestLeaveGroupCall, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestLeaveGroupCall obj)
    {
        throw new NotImplementedException();
    }
}