using MyTelegram.Domain.Aggregates.Poll;
using MyTelegram.Domain.Commands.Poll;
using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SendVoteHandler : RpcResultObjectHandler<RequestSendVote, IUpdates>,
    ISendVoteHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;

    public SendVoteHandler(ICommandBus commandBus,
        IQueryProcessor queryProcessor,
        IPeerHelper peerHelper)
    {
        _commandBus = commandBus;
        _queryProcessor = queryProcessor;
        _peerHelper = peerHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSendVote obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer);
        var pollId = await _queryProcessor.ProcessAsync(new GetPollIdByMessageIdQuery(peer.PeerId, obj.MsgId), default);
        if (pollId == null)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.MessageIdInvalid);
        }

        var command = new VoteCommand(PollId.Create(peer.PeerId, pollId!.Value),
            input.ToRequestInfo(),
            input.UserId,
            obj.Options.Select(p => Encoding.UTF8.GetString(p)).ToList(),
            Guid.NewGuid());
        await _commandBus.PublishAsync(command, default);
        return null!;
    }
}
