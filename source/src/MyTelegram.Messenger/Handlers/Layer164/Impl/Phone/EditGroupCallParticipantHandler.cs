// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Edit information about a given group call participantNote: <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">flags</a>.N?<a href="https://corefork.telegram.org/type/Bool">Bool</a> parameters can have three possible values:
/// <para>Possible errors</para>
/// Code Type Description
/// 403 GROUPCALL_FORBIDDEN The group call has already ended.
/// 400 PARTICIPANT_JOIN_MISSING Trying to enable a presentation, when the user hasn't joined the Video Chat with <a href="https://corefork.telegram.org/method/phone.joinGroupCall">phone.joinGroupCall</a>.
/// 400 USER_VOLUME_INVALID The specified user volume is invalid.
/// See <a href="https://corefork.telegram.org/method/phone.editGroupCallParticipant" />
///</summary>
internal sealed class EditGroupCallParticipantHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestEditGroupCallParticipant, MyTelegram.Schema.IUpdates>,
    Phone.IEditGroupCallParticipantHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestEditGroupCallParticipant obj)
    {
        throw new NotImplementedException();
    }
}
