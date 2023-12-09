// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get poll results
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getPollResults" />
///</summary>
internal sealed class GetPollResultsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPollResults, MyTelegram.Schema.IUpdates>,
    Messages.IGetPollResultsHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IPeerHelper _peerHelper;
    private readonly ILayeredService<IPollConverter> _layeredService;
    private readonly IAccessHashHelper _accessHashHelper;
    public GetPollResultsHandler(IQueryProcessor queryProcessor,
        IPeerHelper peerHelper,
        ILayeredService<IPollConverter> layeredService,
        IAccessHashHelper accessHashHelper)
    {
        _queryProcessor = queryProcessor;
        _peerHelper = peerHelper;
        _layeredService = layeredService;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestGetPollResults obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.Peer);
        var peer = _peerHelper.GetPeer(obj.Peer);
        var pollId = await _queryProcessor.ProcessAsync(new GetPollIdByMessageIdQuery(peer.PeerId, obj.MsgId), default);
        if (pollId == null)
        {
            RpcErrors.RpcErrors400.MessageIdInvalid.ThrowRpcError();
        }

        var pollReadModel = await _queryProcessor
            .ProcessAsync(new GetPollQuery(peer.PeerId, pollId!.Value),
                default);
        if (pollReadModel == null)
        {
            RpcErrors.RpcErrors400.MessageIdInvalid.ThrowRpcError();
        }
        var pollAnswers = await _queryProcessor
                .ProcessAsync(new GetPollAnswerVotersQuery(pollId.Value, input.UserId), default)
            ;
        var updates = _layeredService.GetConverter(input.Layer).ToPollUpdates(pollReadModel!,
            pollAnswers?.Select(p => p.Option).ToArray() ?? Array.Empty<string>());

        return updates;
    }
}
