namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get poll results
/// Possible errors
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getPollResults"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetPollResultsHandler(IQueryProcessor queryProcessor, IPeerHelper peerHelper, //ILayeredService<IPollConverter> layeredService,
 IPollConverterService pollConverterService) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPollResults, MyTelegram.Schema.IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input, RequestGetPollResults obj)
    {
        var peer = peerHelper.GetPeer(obj.Peer);
        var pollId = await queryProcessor.ProcessAsync(new GetPollIdByMessageIdQuery(peer.PeerId, obj.MsgId));
        if (pollId == null)
        {
            RpcErrors.RpcErrors400.MessageIdInvalid.ThrowRpcError();
        }

        var pollReadModel = await queryProcessor.ProcessAsync(new GetPollQuery(pollId!.Value));
        if (pollReadModel == null)
        {
            RpcErrors.RpcErrors400.MessageIdInvalid.ThrowRpcError();
        }

        var pollAnswers = await queryProcessor.ProcessAsync(new GetPollAnswerVotersQuery(pollId.Value, input.UserId), default);
        var updates = pollConverterService.ToPollUpdates(pollReadModel!, pollAnswers?.Select(p => p.Option).ToArray() ?? []);
        return updates;
    }
}