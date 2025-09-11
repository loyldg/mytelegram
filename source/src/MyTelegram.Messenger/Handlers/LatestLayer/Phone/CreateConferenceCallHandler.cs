namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;

///<summary>
/// See <a href="https://corefork.telegram.org/method/phone.createConferenceCall" />
///</summary>
internal sealed class CreateConferenceCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestCreateConferenceCall, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestCreateConferenceCall obj)
    {
        throw new NotImplementedException();
    }
}
