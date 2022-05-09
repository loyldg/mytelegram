using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetMessageReadParticipantsHandler :
    RpcResultObjectHandler<RequestGetMessageReadParticipants, TVector<long>>,
    IGetMessageReadParticipantsHandler, IProcessedHandler
{
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;

    public GetMessageReadParticipantsHandler(IQueryProcessor queryProcessor,
        IPeerHelper peerHelper)
    {
        _queryProcessor = queryProcessor;
        _peerHelper = peerHelper;
    }

    protected override async Task<TVector<long>> HandleCoreAsync(IRequestInput input,
        RequestGetMessageReadParticipants obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer);
        var uidList =
            await _queryProcessor.ProcessAsync(new GetReadingHistoryQuery(peer.PeerId, obj.MsgId), default)
                .ConfigureAwait(false);

        return new TVector<long>(uidList);
    }
}
