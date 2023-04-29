using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetHistoryHandler : RpcResultObjectHandler<RequestGetHistory, IMessages>,
    IGetHistoryHandler, IProcessedHandler
{
    private readonly IMessageAppService _messageAppService;
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public GetHistoryHandler(IMessageAppService messageAppService,
        IQueryProcessor queryProcessor,
        IRpcResultProcessor rpcResultProcessor,
        IPeerHelper peerHelper)
    {
        _messageAppService = messageAppService;
        _queryProcessor = queryProcessor;
        _rpcResultProcessor = rpcResultProcessor;
        _peerHelper = peerHelper;
    }

    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestGetHistory obj)
    {
        var userId = input.UserId;
        var peer = _peerHelper.GetPeer(obj.Peer, userId);
        var ownerPeerId = peer.PeerType == PeerType.Channel ? peer.PeerId : userId;

        if (peer.PeerType == PeerType.Channel)
        {
            var channelMember = await _queryProcessor
                    .ProcessAsync(new GetChannelMemberByUidQuery(peer.PeerId, input.UserId), default)
                ;
            if (channelMember?.Kicked == true)
            {
                return new TChannelMessages
                {
                    Chats = new TVector<IChat>(),
                    Messages = new TVector<IMessage>(),
                    Users = new TVector<IUser>(),
                    Topics = new TVector<IForumTopic>()
                };
            }
        }

        int channelHistoryMinId;
        //if (peer.PeerType == PeerType.Channel || peer.PeerType == PeerType.Chat)
        {
            var dialogReadModel = await _queryProcessor
                    .ProcessAsync(new GetDialogByIdQuery(DialogId.Create(input.UserId, peer)), CancellationToken.None)
                ;
            channelHistoryMinId = dialogReadModel?.ChannelHistoryMinId ?? 0;
        }

        var r = await _messageAppService.GetHistoryAsync(
            new GetHistoryInput(ownerPeerId, userId, _peerHelper.GetPeer(obj.Peer, userId), channelHistoryMinId)
            {
                AddOffset = obj.AddOffset,
                Limit = obj.Limit,
                MaxId = obj.MaxId,
                MinId = obj.MinId,
                OffsetId = obj.OffsetId
            });

        return _rpcResultProcessor.ToMessages(r);
    }
}

public class GetHistoryHandlerLayerN : RpcResultObjectHandler<Schema.LayerN.RequestGetHistory, IMessages>,
    IGetHistoryHandlerLayerN, IProcessedHandler
{
    private readonly IMessageAppService _messageAppService;
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public GetHistoryHandlerLayerN(IMessageAppService messageAppService,
        IQueryProcessor queryProcessor,
        IRpcResultProcessor rpcResultProcessor,
        IPeerHelper peerHelper)
    {
        _messageAppService = messageAppService;
        _queryProcessor = queryProcessor;
        _rpcResultProcessor = rpcResultProcessor;
        _peerHelper = peerHelper;
    }

    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        Schema.LayerN.RequestGetHistory obj)
    {
        var userId = input.UserId;
        var peer = _peerHelper.GetPeer(obj.Peer, userId);
        var ownerPeerId = peer.PeerType == PeerType.Channel ? peer.PeerId : userId;

        int channelHistoryMinId;
        //if (peer.PeerType == PeerType.Channel || peer.PeerType == PeerType.Chat)
        {
            var dialogReadModel = await _queryProcessor
                    .ProcessAsync(new GetDialogByIdQuery(DialogId.Create(input.UserId, peer)), CancellationToken.None)
                ;
            channelHistoryMinId = dialogReadModel?.ChannelHistoryMinId ?? 0;
        }

        var r = await _messageAppService.GetHistoryAsync(
            new GetHistoryInput(ownerPeerId, userId, _peerHelper.GetPeer(obj.Peer, userId), channelHistoryMinId)
            {
                AddOffset = obj.AddOffset,
                Limit = obj.Limit,
                MaxId = obj.MaxId,
                MinId = obj.MinId,
                OffsetId = obj.OffsetId
            });

        return _rpcResultProcessor.ToMessages(r);
    }
}
