namespace MyTelegram.Domain.Aggregates.Poll;

[EnableAutoGeneration]
public class PollAggregate : AggregateRoot<PollAggregate, PollId>
{
    private readonly PollState _state = new();

    public PollAggregate(PollId id) : base(id)
    {
        Register(_state);
    }

    public void Vote(RequestInfo requestInfo, long voteUserPeerId, IReadOnlyCollection<string> options)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (_state.Closed)
        {
            RpcErrors.RpcErrors400.MessagePollClosed.ThrowRpcError();
        }

        if (!_state.MultipleChoice)
        {
            if (options.Count > 1)
            {
                RpcErrors.RpcErrors400.OptionInvalid.ThrowRpcError();
            }
        }

        if (_state.VotedPeerIds.Contains(voteUserPeerId))
        {
            if (_state.Quiz)
            {
                RpcErrors.RpcErrors400.RevoteNotAllowed.ThrowRpcError();
            }
        }


        foreach (var option in options)
        {
            if (!_state.Options.Contains(option))
            {
                RpcErrors.RpcErrors400.OptionInvalid.ThrowRpcError();
            }
        }

        var answerVoters = _state.AnswerVoters;

        // Only quiz==false can retract vote
        List<string>? retractVoteOptions = null;
        if (options.Count == 0 && !_state.Quiz)
        {
            retractVoteOptions = _state.GetVoteOptionsByUserId(voteUserPeerId);
            foreach (var pollAnswerVoter in answerVoters)
            {
                if (retractVoteOptions.Contains(pollAnswerVoter.Option))
                {
                    pollAnswerVoter.DecrementVoters();
                }
            }
        }
        else
        {
            foreach (var answer in answerVoters)
            {
                if (options.Contains(answer.Option))
                {
                    answer.IncrementVoters();
                }
            }
        }

        Emit(new VoteSucceededEvent(
            requestInfo,
            _state.PollId,
            voteUserPeerId,
            options,
            _state.Answers,
            _state.CorrectAnswers,
            answerVoters,
            _state.ToPeer,
            retractVoteOptions
        ));
    }

    public void ClosePoll(int closeDate)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new PollClosedEvent(_state.ToPeer, _state.PollId, closeDate));
    }

    public void CreatePoll(Peer toPeer,
        long pollId,
        bool multipleChoice,
        bool quiz,
        bool publicVoters,
        string question,
        List<PollAnswer> answers,
        IReadOnlyCollection<string>? correctAnswers,
        string? solution,
        //byte[]? solutionEntities,
        //byte[]? questionEntities
        IList<IMessageEntity>? solutionEntities,
        IList<IMessageEntity>? questionEntities,
        long creatorUserId
        )
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        if (answers.Count > MyTelegramConsts.MaxVoteOptions)
        {
            RpcErrors.RpcErrors400.OptionsTooMuch.ThrowRpcError();
        }

        Emit(new PollCreatedEvent(toPeer,
            pollId,
            multipleChoice,
            quiz,
            publicVoters,
            question,
            answers,
            correctAnswers,
            solution,
            solutionEntities,
            questionEntities,
            creatorUserId
            ));
    }

    public void CreateVoteAnswer(long pollId,
        long voterPeerId,
        string option,
        bool correct)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var date = DateTime.UtcNow.ToTimestamp();
        Emit(new VoteAnswerCreatedEvent(pollId, voterPeerId, option, correct, date));
    }

    public void DeleteVoteAnswer(long pollId,
        long voterPeerId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new VoteAnswerDeletedEvent(pollId, voterPeerId));
    }
}