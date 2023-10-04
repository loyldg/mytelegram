using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetDiscussionMessageHandler : RpcResultObjectHandler<RequestGetDiscussionMessage, IDiscussionMessage>,
    IGetDiscussionMessageHandler, IProcessedHandler
{
    private readonly ITlChatConverter _chatConverter;
    private readonly ITlMessageConverter _messageConverter;
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;

    public GetDiscussionMessageHandler(IPeerHelper peerHelper,
        ITlMessageConverter messageConverter,
        IQueryProcessor queryProcessor,
        ITlChatConverter chatConverter)
    {
        _peerHelper = peerHelper;
        _messageConverter = messageConverter;
        _queryProcessor = queryProcessor;
        _chatConverter = chatConverter;
    }

    protected override async Task<IDiscussionMessage> HandleCoreAsync(IRequestInput input,
        RequestGetDiscussionMessage obj)
    {
        // peer is the channel peer
        var peer = _peerHelper.GetPeer(obj.Peer);
        var query = new GetDiscussionMessageQuery(peer.PeerId, obj.MsgId);
        var messageReadModel = await _queryProcessor
                .ProcessAsync(query, default)
            ;

        if (messageReadModel == null)
            return new TDiscussionMessage
            {
                Chats = new TVector<IChat>(),
                Messages = new TVector<IMessage>(),
                Users = new TVector<IUser>()
            };

        var reply = await _queryProcessor.ProcessAsync(new GetReplyQuery(peer.PeerId, obj.MsgId), default)
            ;

        //var messages = _messageConverter.ToMessages(new[] { messageReadModel }, input.UserId);

        var dialogReadModel =
            await _queryProcessor.ProcessAsync(
                new GetDialogByIdQuery(DialogId.Create(input.UserId, PeerType.Channel, messageReadModel.ToPeerId)),
                default);
        var channelReadModels = await _queryProcessor
                .ProcessAsync(new GetChannelByChannelIdListQuery(new[] { peer.PeerId, messageReadModel.ToPeerId }),
                    default)
            ;

        var readMaxId = 0;
        if (dialogReadModel != null)
            readMaxId = Math.Max(dialogReadModel.ReadInboxMaxId, dialogReadModel.ReadOutboxMaxId);

        if (reply?.MaxId > 0 && readMaxId > reply.MaxId) readMaxId = reply.MaxId;

        var message = _messageConverter.ToDiscussionMessage(messageReadModel,
            reply?.MaxId ?? 0,
            readMaxId,
            dialogReadModel?.ReadInboxMaxId ?? 0,
            dialogReadModel?.ReadOutboxMaxId ?? 0,
            input.UserId);
        var chats = _chatConverter.ToChannelList(channelReadModels,
            new List<long>(),
            new List<IChannelMemberReadModel>(),
            input.UserId,
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