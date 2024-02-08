// ReSharper disable All

namespace MyTelegram.Handlers.Messages.LayerN;

///<summary>
/// Returns the conversation history with one interlocutor / within a chat
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getHistory" />
///</summary>
internal sealed class GetHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.LayerN.RequestGetHistory, MyTelegram.Schema.Messages.IMessages>,
    Messages.LayerN.IGetHistoryHandler
{
    private readonly IMessageAppService _messageAppService;
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;
    //private readonly IRpcResultProcessor _rpcResultProcessor;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly ILayeredService<IRpcResultProcessor> _layeredService;
    public GetHistoryHandler(IMessageAppService messageAppService,
        IQueryProcessor queryProcessor,
        //IRpcResultProcessor rpcResultProcessor,
        IPeerHelper peerHelper,
        IAccessHashHelper accessHashHelper,
        ILayeredService<IRpcResultProcessor> layeredService)
    {
        _messageAppService = messageAppService;
        _queryProcessor = queryProcessor;
        //_rpcResultProcessor = rpcResultProcessor;
        _peerHelper = peerHelper;
        _accessHashHelper = accessHashHelper;
        _layeredService = layeredService;
    }

    protected override async Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.LayerN.RequestGetHistory obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.Peer);
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

        var r = await _messageAppService.GetHistoryAsync(new GetHistoryInput
        {
            OwnerPeerId = ownerPeerId,
            SelfUserId = userId,
            AddOffset = obj.AddOffset,
            Limit = obj.Limit,
            MaxId = obj.MaxId,
            MinId = obj.MinId,
            OffsetId = obj.OffsetId,
            Peer = _peerHelper.GetPeer(obj.Peer, userId),
            ChannelHistoryMinId = channelHistoryMinId
        });

        return _layeredService.GetConverter(input.Layer).ToMessages(r, input.Layer);
    }
}
