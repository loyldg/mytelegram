// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get info about the users that joined the chat using a specific chat invite
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 SEARCH_WITH_LINK_NOT_SUPPORTED You cannot provide a search query and an invite link at the same time.
/// See <a href="https://corefork.telegram.org/method/messages.getChatInviteImporters" />
///</summary>
internal sealed class GetChatInviteImportersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetChatInviteImporters, MyTelegram.Schema.Messages.IChatInviteImporters>,
    Messages.IGetChatInviteImportersHandler
{
    protected override Task<MyTelegram.Schema.Messages.IChatInviteImporters> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetChatInviteImporters obj)
    {
        throw new NotImplementedException();
    }
}
