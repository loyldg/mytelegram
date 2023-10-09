// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get info about the chat invites of a specific chat
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ADMIN_ID_INVALID The specified admin ID is invalid.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getExportedChatInvites" />
///</summary>
internal sealed class GetExportedChatInvitesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetExportedChatInvites, MyTelegram.Schema.Messages.IExportedChatInvites>,
    Messages.IGetExportedChatInvitesHandler
{
    protected override Task<MyTelegram.Schema.Messages.IExportedChatInvites> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetExportedChatInvites obj)
    {
        throw new NotImplementedException();
    }
}
