using MyTelegram.Domain.Events.Poll;

namespace MyTelegram.Domain.Aggregates.Poll;

public class PollAggregate : AggregateRoot<PollAggregate, PollId>
{
    private readonly PollState _state = new();

    public PollAggregate(PollId id) : base(id)
    {
        Register(_state);
    }

    public void Vote(RequestInfo request, long voteUserPeerId, IReadOnlyCollection<string> options, Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (_state.Closed)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.MessagePollClosed);
        }

        if (!_state.MultipleChoice)
        {
            if (options.Count > 1)
            {
                ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.OptionInvalid);
            }
        }

        if (_state.VotedPeerIds.Contains(voteUserPeerId))
        {
            if (_state.Quiz)
            {
                ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.RevoteNotAllowed);
            }
        }


        foreach (var option in options)
        {
            if (!_state.Options.Contains(option))
            {
                ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.OptionInvalid);
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
            request,
            _state.PollId,
            voteUserPeerId,
            options,
            _state.Answers,
            _state.CorrectAnswers,
            answerVoters,
            _state.ToPeer,
            retractVoteOptions,
            correlationId
        ));
    }

    public void Close(int closeDate)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new PollClosedEvent(_state.ToPeer, _state.PollId, closeDate));
    }

    public void Create(Peer toPeer,
        long pollId,
        bool multipleChoice,
        bool quiz,
        bool publicVoters,
        string question,
        IReadOnlyCollection<PollAnswer> answers,
        IReadOnlyCollection<string>? correctAnswers,
        string? solution,
        byte[]? solutionEntities)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        if (answers.Count > MyTelegramServerDomainConsts.MaxVoteOptions)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.OptionsTooMuch);
        }

        Emit(new PollCreatedEvent(toPeer,
            pollId,
            multipleChoice,
            quiz,
            publicVoters,
            question,
            answers.ToList(),
            correctAnswers,
            solution,
            solutionEntities));
    }

    public void CreateVoteAnswer(long pollId,
        long voterPeerId,
        string option,
        bool correct)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new VoteAnswerCreatedEvent(pollId, voterPeerId, option, correct));
    }

    public void DeleteVoteAnswer(long pollId,
        long voterPeerId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new VoteAnswerDeletedEvent(pollId, voterPeerId));
    }
}