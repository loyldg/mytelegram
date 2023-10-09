// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Export an invite link for a chat
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 EXPIRE_DATE_INVALID The specified expiration date is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USAGE_LIMIT_INVALID The specified usage limit is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.exportChatInvite" />
///</summary>
internal sealed class ExportChatInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestExportChatInvite, MyTelegram.Schema.IExportedChatInvite>,
    Messages.IExportChatInviteHandler
{
    protected override Task<MyTelegram.Schema.IExportedChatInvite> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestExportChatInvite obj)
    {
        throw new NotImplementedException();
    }
}
