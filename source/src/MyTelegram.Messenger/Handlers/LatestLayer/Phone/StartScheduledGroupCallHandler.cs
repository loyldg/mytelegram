namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;

///<summary>
/// Start a scheduled group call.
/// See <a href="https://corefork.telegram.org/method/phone.startScheduledGroupCall" />
///</summary>
internal sealed class StartScheduledGroupCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestStartScheduledGroupCall, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestStartScheduledGroupCall obj)
    {
        throw new NotImplementedException();
    }
}
