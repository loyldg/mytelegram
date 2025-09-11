namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;

///<summary>
/// Stop screen sharing in a group call
/// See <a href="https://corefork.telegram.org/method/phone.leaveGroupCallPresentation" />
///</summary>
internal sealed class LeaveGroupCallPresentationHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestLeaveGroupCallPresentation, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestLeaveGroupCallPresentation obj)
    {
        throw new NotImplementedException();
    }
}
