namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Remove participants from a conference call.Exactly one of the <code>only_left</code> and <code>kick</code> flags must be set.
/// Possible errors
/// Code Type Description
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.deleteConferenceCallParticipants"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DeleteConferenceCallParticipantsHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestDeleteConferenceCallParticipants, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestDeleteConferenceCallParticipants obj)
    {
        throw new NotImplementedException();
    }
}