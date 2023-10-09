// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Get full info about a <a href="https://corefork.telegram.org/api/channel#supergroups">supergroup</a>, <a href="https://corefork.telegram.org/api/channel#gigagroups">gigagroup</a> or <a href="https://corefork.telegram.org/api/channel#channels">channel</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHANNEL_PUBLIC_GROUP_NA channel/supergroup not available.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/channels.getFullChannel" />
///</summary>
internal sealed class GetFullChannelHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetFullChannel, MyTelegram.Schema.Messages.IChatFull>,
    Channels.IGetFullChannelHandler
{
    protected override Task<MyTelegram.Schema.Messages.IChatFull> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetFullChannel obj)
    {
        throw new NotImplementedException();
    }
}
