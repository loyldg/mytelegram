namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;

///<summary>
/// See <a href="https://corefork.telegram.org/method/phone.inviteConferenceCallParticipant" />
///</summary>
internal sealed class InviteConferenceCallParticipantHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestInviteConferenceCallParticipant, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestInviteConferenceCallParticipant obj)
    {
        throw new NotImplementedException();
    }
}
