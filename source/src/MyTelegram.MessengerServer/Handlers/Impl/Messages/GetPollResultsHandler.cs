using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetPollResultsHandler : RpcResultObjectHandler<RequestGetPollResults, IUpdates>,
    IGetPollResultsHandler, IProcessedHandler
{
    private readonly IPeerHelper _peerHelper;
    private readonly ITlPollConverter _pollConverter;
    private readonly IQueryProcessor _queryProcessor;

    public GetPollResultsHandler(IQueryProcessor queryProcessor,
        IPeerHelper peerHelper,
        ITlPollConverter pollConverter)
    {
        _queryProcessor = queryProcessor;
        _peerHelper = peerHelper;
        _pollConverter = pollConverter;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestGetPollResults obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer);
        var pollId = await _queryProcessor.ProcessAsync(new GetPollIdByMessageIdQuery(peer.PeerId, obj.MsgId), default);
        if (pollId == null) ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.MessageIdInvalid);

        var pollReadModel = await _queryProcessor
            .ProcessAsync(new GetPollQuery(peer.PeerId, pollId!.Value),
                default);
        if (pollReadModel == null) ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.MessageIdInvalid);

        var pollAnswers = await _queryProcessor
                .ProcessAsync(new GetPollAnswerVotersQuery(pollId.Value, input.UserId), default)
            ;
        var updates = _pollConverter.ToPollUpdates(pollReadModel!,
            pollAnswers?.Select(p => p.Option).ToArray() ?? Array.Empty<string>());

        return updates;
    }
}