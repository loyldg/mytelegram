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
    Messages.LayerN.IGetHistoryHandler, IProcessedHandler
{
    private readonly IMessageAppService _messageAppService;
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public GetHistoryHandler(IMessageAppService messageAppService, IPeerHelper peerHelper, IQueryProcessor queryProcessor, IRpcResultProcessor rpcResultProcessor)
    {
        _messageAppService = messageAppService;
        _peerHelper = peerHelper;
        _queryProcessor = queryProcessor;
        _rpcResultProcessor = rpcResultProcessor;
    }

    protected override async Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.LayerN.RequestGetHistory obj)
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
