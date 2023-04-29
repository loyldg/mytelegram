using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetMessageReadParticipantsHandler :
    RpcResultObjectHandler<RequestGetMessageReadParticipants, TVector<IReadParticipantDate>>,
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

    protected override async Task<TVector<IReadParticipantDate>> HandleCoreAsync(IRequestInput input,
        RequestGetMessageReadParticipants obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer);
        var readModels = await _queryProcessor
            .ProcessAsync(new GetMessageReadParticipantsQuery(peer.PeerId, obj.MsgId), default);

        return new TVector<IReadParticipantDate>(readModels.Select(p => new TReadParticipantDate
        {
            Date = p.Date,
            UserId = p.ReaderPeerId,
        }));
    }
}
