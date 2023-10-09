// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Dismiss or approve a chat <a href="https://corefork.telegram.org/api/invites#join-requests">join request</a> related to a specific chat or channel.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNELS_TOO_MUCH You have joined too many channels/supergroups.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 HIDE_REQUESTER_MISSING The join request was missing or was already handled.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_ALREADY_PARTICIPANT The user is already in the group.
/// 403 USER_CHANNELS_TOO_MUCH One of the users you tried to add is already in too many channels/supergroups.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.hideChatJoinRequest" />
///</summary>
internal sealed class HideChatJoinRequestHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestHideChatJoinRequest, MyTelegram.Schema.IUpdates>,
    Messages.IHideChatJoinRequestHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestHideChatJoinRequest obj)
    {
        throw new NotImplementedException();
    }
}
