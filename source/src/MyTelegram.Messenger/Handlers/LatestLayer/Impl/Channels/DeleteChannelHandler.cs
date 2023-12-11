// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Delete a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 406 CHANNEL_TOO_LARGE Channel is too large to be deleted; this error is issued when trying to delete channels with more than 1000 members (subject to change).
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// See <a href="https://corefork.telegram.org/method/channels.deleteChannel" />
///</summary>
internal sealed class DeleteChannelHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestDeleteChannel, MyTelegram.Schema.IUpdates>,
    Channels.IDeleteChannelHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestDeleteChannel obj)
    {
        throw new NotImplementedException();
    }
}
