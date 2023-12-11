// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Get the admin log of a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/channels.getAdminLog" />
///</summary>
internal sealed class GetAdminLogHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetAdminLog, MyTelegram.Schema.Channels.IAdminLogResults>,
    Channels.IGetAdminLogHandler
{
    protected override Task<MyTelegram.Schema.Channels.IAdminLogResults> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetAdminLog obj)
    {
        throw new NotImplementedException();
    }
}
