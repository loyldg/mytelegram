namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;

///<summary>
/// See <a href="https://corefork.telegram.org/method/phone.sendConferenceCallBroadcast" />
///</summary>
internal sealed class SendConferenceCallBroadcastHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestSendConferenceCallBroadcast, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestSendConferenceCallBroadcast obj)
    {
        throw new NotImplementedException();
    }
}
