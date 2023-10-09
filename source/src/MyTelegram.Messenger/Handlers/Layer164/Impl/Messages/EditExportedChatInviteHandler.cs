// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Edit an exported chat invite
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_INVITE_PERMANENT You can't set an expiration date on permanent invite links.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 403 EDIT_BOT_INVITE_FORBIDDEN Normal users can't edit invites that were created by bots.
/// 400 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.editExportedChatInvite" />
///</summary>
internal sealed class EditExportedChatInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestEditExportedChatInvite, MyTelegram.Schema.Messages.IExportedChatInvite>,
    Messages.IEditExportedChatInviteHandler
{
    protected override Task<MyTelegram.Schema.Messages.IExportedChatInvite> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestEditExportedChatInvite obj)
    {
        throw new NotImplementedException();
    }
}
