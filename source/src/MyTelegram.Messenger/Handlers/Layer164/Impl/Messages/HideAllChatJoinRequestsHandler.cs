// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Dismiss or approve all <a href="https://corefork.telegram.org/api/invites#join-requests">join requests</a> related to a specific chat or channel.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNELS_TOO_MUCH You have joined too many channels/supergroups.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 HIDE_REQUESTER_MISSING The join request was missing or was already handled.
/// 400 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_CHANNELS_TOO_MUCH One of the users you tried to add is already in too many channels/supergroups.
/// See <a href="https://corefork.telegram.org/method/messages.hideAllChatJoinRequests" />
///</summary>
internal sealed class HideAllChatJoinRequestsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestHideAllChatJoinRequests, MyTelegram.Schema.IUpdates>,
    Messages.IHideAllChatJoinRequestsHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestHideAllChatJoinRequests obj)
    {
        throw new NotImplementedException();
    }
}
