// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Get the participants of a <a href="https://corefork.telegram.org/api/channel">supergroup/channel</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// See <a href="https://corefork.telegram.org/method/channels.getParticipants" />
///</summary>
internal sealed class GetParticipantsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetParticipants, MyTelegram.Schema.Channels.IChannelParticipants>,
    Channels.IGetParticipantsHandler
{
    protected override Task<MyTelegram.Schema.Channels.IChannelParticipants> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetParticipants obj)
    {
        throw new NotImplementedException();
    }
}
