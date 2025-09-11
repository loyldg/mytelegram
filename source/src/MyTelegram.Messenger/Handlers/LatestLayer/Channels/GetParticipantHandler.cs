namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

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
internal sealed class GetParticipantHandler(
    IQueryProcessor queryProcessor,
    IPeerHelper peerHelper,
    IChannelAppService channelAppService,
    IChatConverterService chatConverterService,
    IUserConverterService userConverterService,
    IAccessHashHelper accessHashHelper,
    IPhotoAppService photoAppService)
    : RpcResultObjectHandler<RequestGetParticipant,
            MyTelegram.Schema.Channels.IChannelParticipant>
{
    protected override async Task<MyTelegram.Schema.Channels.IChannelParticipant> HandleCoreAsync(IRequestInput input,
        RequestGetParticipant obj)
    {
        var peer = peerHelper.GetPeer(obj.Participant, input.UserId);
        if (obj.Channel is TInputChannel inputChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(input, inputChannel.ChannelId, inputChannel.AccessHash, AccessHashType.Channel);

            var channelMemberReadModel = await queryProcessor
                .ProcessAsync(new GetChannelMemberByUserIdQuery(inputChannel.ChannelId, peer.PeerId));
            if (channelMemberReadModel == null)
            {
                RpcErrors.RpcErrors400.UserNotParticipant.ThrowRpcError();
            }

            var userId = channelMemberReadModel?.UserId ?? input.UserId;

            var channelReadModel = await channelAppService.GetAsync(inputChannel.ChannelId);
            channelReadModel.ThrowExceptionIfChannelDeleted();
            var user = await userConverterService.GetUserAsync(input, userId, false, false, input.Layer);

            var photoReadModel = await photoAppService.GetAsync(channelReadModel!.PhotoId);
            var r = chatConverterService.ToChannelParticipant(
                input,
                channelReadModel,
                photoReadModel,
                channelMemberReadModel!,
                user,
                input.Layer
                );
            return r;
        }

        throw new NotImplementedException();
    }
}