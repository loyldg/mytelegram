namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/threads">discussion message</a> from the <a href="https://corefork.telegram.org/api/discussion">associated discussion group</a> of a channel to show it on top of the comment section, without actually joining the group
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 TOPIC_ID_INVALID The specified topic ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getDiscussionMessage" />
///</summary>
internal sealed class GetDiscussionMessageHandler(
    IPeerHelper peerHelper,
    IQueryProcessor queryProcessor,
    IChannelAppService channelAppService,
    //ILayeredService<IChannelConverter> layeredChatService,
    IChatConverterService chatConverterService,
    IMessageConverterService messageConverterService,
    IAccessHashHelper accessHashHelper,
    IPhotoAppService photoAppService)
    : RpcResultObjectHandler<RequestGetDiscussionMessage,
            IDiscussionMessage>
{
    protected override async Task<IDiscussionMessage> HandleCoreAsync(IRequestInput input,
        RequestGetDiscussionMessage obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Peer);
        // peer is the channel peer
        var peer = peerHelper.GetPeer(obj.Peer);
        var channelReadModel = await channelAppService.GetAsync(peer.PeerId);
        if (channelReadModel == null!)
        {
            RpcErrors.RpcErrors400.ChatIdInvalid.ThrowRpcError();
        }
        var query = new GetDiscussionMessageQuery(channelReadModel!.Broadcast, peer.PeerId, obj.MsgId);

        var messageReadModel = await queryProcessor
            .ProcessAsync(query);

        if (messageReadModel == null)
        {
            return new TDiscussionMessage
            {
                Chats = [],
                Messages = [],
                Users = [],
            };
        }

        var dialogReadModel =
            await queryProcessor.ProcessAsync(new GetDialogByIdQuery(DialogId.Create(input.UserId, PeerType.Channel, messageReadModel.ToPeerId).Value), default);

        List<long> channelIds = [peer.PeerId, messageReadModel.ToPeerId];
        var channelReadModels = await channelAppService.GetListAsync(channelIds);

        var readMaxId = 0;
        if (dialogReadModel != null)
        {
            readMaxId = Math.Max(dialogReadModel.ReadInboxMaxId, dialogReadModel.ReadOutboxMaxId);
        }

        var message = messageConverterService.ToMessage(input.UserId, messageReadModel, layer: input.Layer);
        var photoReadModels = await photoAppService.GetPhotosAsync(channelReadModels);
        var channelMemberReadModels =
            await queryProcessor.ProcessAsync(new GetChannelMemberListByChannelIdListQuery(input.UserId, channelIds));

        var chats = chatConverterService.ToChannelList(
            input,
            channelReadModels,
            photoReadModels,
            channelMemberReadModels,
              layer: input.Layer);

        return new TDiscussionMessage
        {
            Chats = [.. chats],
            Messages = new TVector<IMessage>(message),
            Users = [],
            MaxId = readMaxId,
            ReadInboxMaxId = dialogReadModel?.ReadInboxMaxId,
            ReadOutboxMaxId = dialogReadModel?.ReadOutboxMaxId
        };
    }
}
