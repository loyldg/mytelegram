namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Edit information about a given group call participantNote: <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">flags</a>.N?<a href="https://corefork.telegram.org/type/Bool">Bool</a> parameters can have three possible values:
/// Possible errors
/// Code Type Description
/// 403 GROUPCALL_FORBIDDEN The group call has already ended.
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// 400 PARTICIPANT_JOIN_MISSING Trying to enable a presentation, when the user hasn't joined the Video Chat with <a href="https://corefork.telegram.org/method/phone.joinGroupCall">phone.joinGroupCall</a>.
/// 400 RAISE_HAND_FORBIDDEN You cannot raise your hand.
/// 400 USER_VOLUME_INVALID The specified user volume is invalid.
/// 400 VIDEO_PAUSE_FORBIDDEN You cannot pause the video stream.
/// 400 VIDEO_STOP_FORBIDDEN You cannot stop the video stream.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.editGroupCallParticipant"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class EditGroupCallParticipantHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestEditGroupCallParticipant, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestEditGroupCallParticipant obj)
    {
        throw new NotImplementedException();
    }
}