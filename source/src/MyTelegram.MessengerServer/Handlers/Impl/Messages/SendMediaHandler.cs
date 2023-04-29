using MyTelegram.Domain.Aggregates.Poll;
using MyTelegram.Domain.Commands.Poll;
using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SendMediaHandler : RpcResultObjectHandler<RequestSendMedia, IUpdates>,
    ISendMediaHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IMediaHelper _mediaHelper;
    private readonly IMessageAppService _messageAppService;
    private readonly IPeerHelper _peerHelper;
    private readonly IRandomHelper _randomHelper;

    public SendMediaHandler(IMediaHelper mediaHelper,
        IMessageAppService messageAppService,
        IPeerHelper peerHelper,
        IRandomHelper randomHelper,
        ICommandBus commandBus)
    {
        _mediaHelper = mediaHelper;
        _messageAppService = messageAppService;
        _peerHelper = peerHelper;
        _randomHelper = randomHelper;
        _commandBus = commandBus;
    }

    private async Task CreatePollAsync(Peer toPeer,
        TInputMediaPoll inputMediaPoll)
    {
        var poll = inputMediaPoll.Poll;
        var command = new CreatePollCommand(PollId.Create(toPeer.PeerId, poll.Id),
            toPeer,
            poll.Id,
            poll.MultipleChoice,
            poll.Quiz,
            inputMediaPoll.Poll.PublicVoters,
            poll.Question,
            poll.Answers.Select(p => new PollAnswer(p.Text, p.Option)).ToList(),
            inputMediaPoll.CorrectAnswers?.ToList(),
            inputMediaPoll.Solution,
            inputMediaPoll.SolutionEntities.ToBytes()
        );
        await _commandBus.PublishAsync(command, default);
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSendMedia obj)
    {
        var toPeer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        long? pollId = null;
        if (obj.Media is TInputMediaPoll inputMediaPoll)
        {
            pollId = _randomHelper.NextLong();
            inputMediaPoll.Poll.Id = pollId.Value;
            await CreatePollAsync(toPeer, inputMediaPoll);
        }

        var media = await _mediaHelper.SaveMediaAsync(obj.Media);
        var sendMessageInput = new SendMessageInput(input.ToRequestInfo(),
            input.UserId,
            _peerHelper.GetPeer(obj.Peer, input.UserId),
            obj.Message,
            obj.RandomId,
            clearDraft: obj.ClearDraft,
            entities: obj.Entities.ToBytes(),
            media: media.ToBytes(),
            replyToMsgId: obj.ReplyToMsgId,
            sendMessageType: SendMessageType.Media,
            messageType: _mediaHelper.GeMessageType(media),
            pollId: pollId
        );
        await _messageAppService.SendMessageAsync(sendMessageInput);

        return null!;
    }
}
