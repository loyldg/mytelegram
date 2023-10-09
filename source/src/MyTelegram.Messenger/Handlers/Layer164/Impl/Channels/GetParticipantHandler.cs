// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Get info about a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a> participant
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PARTICIPANT_ID_INVALID The specified participant ID is invalid.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// 400 USER_NOT_PARTICIPANT You're not a member of this supergroup/channel.
/// See <a href="https://corefork.telegram.org/method/channels.getParticipant" />
///</summary>
internal sealed class GetParticipantHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetParticipant, MyTelegram.Schema.Channels.IChannelParticipant>,
    Channels.IGetParticipantHandler
{
    protected override Task<MyTelegram.Schema.Channels.IChannelParticipant> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetParticipant obj)
    {
        throw new NotImplementedException();
    }
}
