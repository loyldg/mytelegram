namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;

///<summary>
/// See <a href="https://corefork.telegram.org/method/phone.declineConferenceCallInvite" />
///</summary>
internal sealed class DeclineConferenceCallInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestDeclineConferenceCallInvite, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestDeclineConferenceCallInvite obj)
    {
        throw new NotImplementedException();
    }
}
