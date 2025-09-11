namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Obtains a list of peers that can be used to send messages in a specific group
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.getSendAs" />
///</summary>
internal sealed class GetSendAsHandler(
    IQueryProcessor queryProcessor,
    IChatConverterService chatConverterService,
    IChannelAppService channelAppService,
    ILayeredService<ISendAsPeerConverter> layeredSendAsPeerService,
    IAccessHashHelper accessHashHelper,
    IMessageAppService messageAppService,
    IPhotoAppService photoAppService)
    : RpcResultObjectHandler<RequestGetSendAs, ISendAsPeers>
{
    protected override async Task<ISendAsPeers> HandleCoreAsync(IRequestInput input,
        RequestGetSendAs obj)
    {
        if (obj.Peer is TInputPeerChannel inputPeerChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(input, inputPeerChannel.ChannelId, inputPeerChannel.AccessHash, AccessHashType.Channel);

            var canSendAsPeer = await messageAppService.CanSendAsPeerAsync(inputPeerChannel.ChannelId, input.UserId);
            var channelReadModel = await channelAppService.GetAsync(inputPeerChannel.ChannelId);
            if (canSendAsPeer)
            {
                var myPublicChannelReadModels = (await queryProcessor.ProcessAsync(new GetSendAsQuery(input.UserId))).ToList();
                var channelIds = myPublicChannelReadModels.Select(p => p.ChannelId).ToList();
                if (!channelIds.Contains(channelReadModel.ChannelId))
                {
                    myPublicChannelReadModels.Insert(0, channelReadModel);
                    channelIds.Add(channelReadModel.ChannelId);
                }
                var channelMemberReadModels = await queryProcessor.ProcessAsync(
                    new GetChannelMemberListByChannelIdListQuery(input.UserId, channelIds));
                var photoReadModels = await photoAppService.GetPhotosAsync(myPublicChannelReadModels);
                var channels = chatConverterService.ToChannelList(input, myPublicChannelReadModels,
                    photoReadModels, channelMemberReadModels, layer: input.Layer);
                var layeredResult = layeredSendAsPeerService.GetConverter(input.Layer).ToSendAsPeers(channels);

                var admin = channelReadModel.AdminList.FirstOrDefault(p => p.UserId == input.UserId);

                if (channelReadModel.Broadcast || admin is { AdminRights.Anonymous: false })
                {
                    layeredResult.Peers.Insert(0, new TSendAsPeer
                    {
                        Peer = new TPeerUser
                        {
                            UserId = input.UserId
                        }
                    });
                }

                // Discussion group: Discussion group + Public channels
                return layeredResult;
            }
        }

        return new TSendAsPeers
        {
            Chats = [],
            Peers = [],
            Users = []
        };
    }
}
