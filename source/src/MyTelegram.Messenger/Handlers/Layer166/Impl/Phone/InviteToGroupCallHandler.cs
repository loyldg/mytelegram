// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Invite a set of users to a group call.
/// <para>Possible errors</para>
/// Code Type Description
/// 403 GROUPCALL_FORBIDDEN The group call has already ended.
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// 400 INVITE_FORBIDDEN_WITH_JOINAS If the user has anonymously joined a group call as a channel, they can't invite other users to the group call because that would cause deanonymization, because the invite would be sent using the original user ID, not the anonymized channel ID.
/// 400 USER_ALREADY_INVITED You have already invited this user.
/// See <a href="https://corefork.telegram.org/method/phone.inviteToGroupCall" />
///</summary>
internal sealed class InviteToGroupCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestInviteToGroupCall, MyTelegram.Schema.IUpdates>,
    Phone.IInviteToGroupCallHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestInviteToGroupCall obj)
    {
        throw new NotImplementedException();
    }
}
