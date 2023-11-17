// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get info about a chat invite
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getExportedChatInvite" />
///</summary>
internal sealed class GetExportedChatInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetExportedChatInvite, MyTelegram.Schema.Messages.IExportedChatInvite>,
    Messages.IGetExportedChatInviteHandler
{
    protected override Task<MyTelegram.Schema.Messages.IExportedChatInvite> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetExportedChatInvite obj)
    {
        throw new NotImplementedException();
    }
}
