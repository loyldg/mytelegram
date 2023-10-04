// ReSharper disable All

using IChannelParticipant = MyTelegram.Schema.Channels.IChannelParticipant;
using RequestGetParticipant = MyTelegram.Schema.Channels.LayerN.RequestGetParticipant;

namespace MyTelegram.Handlers.Channels.LayerN;

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
internal sealed class GetParticipantHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.LayerN.RequestGetParticipant, MyTelegram.Schema.Channels.IChannelParticipant>,
    Channels.LayerN.IGetParticipantHandler, IProcessedHandler
{
    private readonly ITlChatConverter _chatConverter;
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;

    public GetParticipantHandler(IQueryProcessor queryProcessor,
        IPeerHelper peerHelper,
        ITlChatConverter chatConverter)
    {
        _queryProcessor = queryProcessor;
        _peerHelper = peerHelper;
        _chatConverter = chatConverter;
    }

    protected override async Task<MyTelegram.Schema.Channels.IChannelParticipant> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.LayerN.RequestGetParticipant obj)
    {
        var peer = _peerHelper.GetPeer(obj.UserId, input.UserId);
        if (obj.Channel is TInputChannel inputChannel)
        {
            var channelMemberReadModel = await _queryProcessor
                    .ProcessAsync(new GetChannelMemberByUidQuery(inputChannel.ChannelId, peer.PeerId), default)
                ;

            if (channelMemberReadModel == null) ThrowHelper.ThrowUserFriendlyException("USER_NOT_PARTICIPANT");

            var userReadModel = await _queryProcessor
                    .ProcessAsync(new GetUserByIdQuery(channelMemberReadModel!.UserId), default)
                ;

            if (userReadModel == null) ThrowHelper.ThrowUserFriendlyException("USER_ID_INVALID");

            var channelReadModel = await _queryProcessor
                .ProcessAsync(new GetChannelByIdQuery(inputChannel.ChannelId), default);

            return _chatConverter.ToChannelParticipantLayerN(channelReadModel,
                channelMemberReadModel,
                userReadModel!,
                input.UserId);
        }

        throw new NotImplementedException();
    }
}
