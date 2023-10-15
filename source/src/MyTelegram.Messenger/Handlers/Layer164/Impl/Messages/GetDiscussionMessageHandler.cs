// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

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
internal sealed class GetDiscussionMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDiscussionMessage, MyTelegram.Schema.Messages.IDiscussionMessage>,
    Messages.IGetDiscussionMessageHandler
{
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IChatConverter> _layeredChatService;
    private readonly ILayeredService<IMessageConverter> _layeredMessageService;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IPhotoAppService _photoAppService;
    public GetDiscussionMessageHandler(IPeerHelper peerHelper,
        IQueryProcessor queryProcessor,
        ILayeredService<IChatConverter> layeredChatService,
        ILayeredService<IMessageConverter> layeredMessageService,
        IAccessHashHelper accessHashHelper, IPhotoAppService photoAppService)
    {
        _peerHelper = peerHelper;
        _queryProcessor = queryProcessor;
        _layeredChatService = layeredChatService;
        _layeredMessageService = layeredMessageService;
        _accessHashHelper = accessHashHelper;
        _photoAppService = photoAppService;
    }

    protected override async Task<IDiscussionMessage> HandleCoreAsync(IRequestInput input,
        RequestGetDiscussionMessage obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.Peer);
        // peer is the channel peer
        var peer = _peerHelper.GetPeer(obj.Peer);
        var query = new GetDiscussionMessageQuery(peer.PeerId, obj.MsgId);
        var messageReadModel = await _queryProcessor
            .ProcessAsync(query, default)
     ;

        if (messageReadModel == null)
        {
            return new TDiscussionMessage
            {
                Chats = new TVector<IChat>(),
                Messages = new TVector<IMessage>(),
                Users = new TVector<IUser>(),
            };
        }

        var reply = await _queryProcessor.ProcessAsync(new GetReplyQuery(peer.PeerId, obj.MsgId), default)
     ;

        //var messages = _messageConverter.ToMessages(new[] { messageReadModel }, input.UserId);

        var dialogReadModel =
            await _queryProcessor.ProcessAsync(new GetDialogByIdQuery(DialogId.Create(input.UserId, PeerType.Channel, messageReadModel.ToPeerId)), default);
        var channelReadModels = await _queryProcessor
            .ProcessAsync(new GetChannelByChannelIdListQuery(new long[] { peer.PeerId, messageReadModel.ToPeerId }), default)
     ;

        var readMaxId = 0;
        if (dialogReadModel != null)
        {
            readMaxId = Math.Max(dialogReadModel.ReadInboxMaxId, dialogReadModel.ReadOutboxMaxId);
        }

        if (reply?.MaxId > 0 && readMaxId > reply.MaxId)
        {
            readMaxId = reply.MaxId;
        }

        var message = _layeredMessageService.GetConverter(input.Layer).ToDiscussionMessage(messageReadModel,
            reply?.MaxId ?? 0,
            readMaxId,
            dialogReadModel?.ReadInboxMaxId ?? 0,
            dialogReadModel?.ReadOutboxMaxId ?? 0,
            input.UserId);

        var photoReadModels = await _photoAppService.GetPhotosAsync(channelReadModels);

        var chats = _layeredChatService.GetConverter(input.Layer).ToChannelList(
            input.UserId,
            channelReadModels,
            photoReadModels,
            new List<long>(),
            new List<IChannelMemberReadModel>(),
             true);

        return new TDiscussionMessage
        {
            Chats = new TVector<IChat>(chats),
            Messages = new TVector<IMessage>(message),
            Users = new TVector<IUser>(),
            MaxId = reply?.MaxId ?? 0,
            ReadInboxMaxId = dialogReadModel?.ReadInboxMaxId,
            ReadOutboxMaxId = dialogReadModel?.ReadOutboxMaxId
        };
    }
}
