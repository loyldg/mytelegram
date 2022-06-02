using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class GetGroupCallJoinAsHandler : RpcResultObjectHandler<RequestGetGroupCallJoinAs, IJoinAsPeers>,
    IGetGroupCallJoinAsHandler, IProcessedHandler
{
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly ITlPeerConverter _peerConverter;

    public GetGroupCallJoinAsHandler(IQueryProcessor queryProcessor,
        IPeerHelper peerHelper,
        ITlPeerConverter peerConverter)
    {
        _queryProcessor = queryProcessor;
        _peerHelper = peerHelper;
        _peerConverter = peerConverter;
    }

    protected override async Task<IJoinAsPeers> HandleCoreAsync(IRequestInput input,
        RequestGetGroupCallJoinAs obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        var userReadModel = await _queryProcessor
            .ProcessAsync(new GetUserByIdQuery(input.UserId), CancellationToken.None)
            .ConfigureAwait(false);
        IChatReadModel? chatReadModel = null;
        IChannelReadModel? channelReadModel = null;
        switch (peer.PeerType)
        {
            case PeerType.Channel:
                channelReadModel = await _queryProcessor
                    .ProcessAsync(new GetChannelByIdQuery(peer.PeerId), CancellationToken.None)
                    .ConfigureAwait(false);
                break;
            case PeerType.Chat:
                chatReadModel = await _queryProcessor
                    .ProcessAsync(new GetChatByChatIdQuery(peer.PeerId), CancellationToken.None)
                    .ConfigureAwait(false);
                break;
        }

        return _peerConverter.ToJoinAsPeers(userReadModel!, channelReadModel, chatReadModel);
    }
}
