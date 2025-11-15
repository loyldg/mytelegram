namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Stop screen sharing in a group call
/// Possible errors
/// Code Type Description
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.leaveGroupCallPresentation"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class LeaveGroupCallPresentationHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestLeaveGroupCallPresentation, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestLeaveGroupCallPresentation obj)
    {
        throw new NotImplementedException();
    }
}