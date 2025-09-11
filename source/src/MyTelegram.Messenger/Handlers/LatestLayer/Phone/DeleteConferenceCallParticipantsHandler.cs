namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;

///<summary>
/// See <a href="https://corefork.telegram.org/method/phone.deleteConferenceCallParticipants" />
///</summary>
internal sealed class DeleteConferenceCallParticipantsHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestDeleteConferenceCallParticipants, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestDeleteConferenceCallParticipants obj)
    {
        throw new NotImplementedException();
    }
}
