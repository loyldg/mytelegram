// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Get info about <a href="https://corefork.telegram.org/api/channel">channels/supergroups</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// See <a href="https://corefork.telegram.org/method/channels.getChannels" />
///</summary>
internal sealed class GetChannelsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetChannels, MyTelegram.Schema.Messages.IChats>,
    Channels.IGetChannelsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IChats> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetChannels obj)
    {
        throw new NotImplementedException();
    }
}
