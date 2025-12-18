namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Invite a user to a conference call.
/// Possible errors
/// Code Type Description
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.inviteConferenceCallParticipant"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class InviteConferenceCallParticipantHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestInviteConferenceCallParticipant, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestInviteConferenceCallParticipant obj)
    {
        throw new NotImplementedException();
    }
}