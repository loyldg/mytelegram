namespace MyTelegram.Domain.Aggregates.Poll;

public record PollSnapshot(long PollId, List<string> Options, List<IPollAnswer> Answers2, bool Closed, bool Quiz, bool MultipleChoice, HashSet<long> VotedPeerIds, Peer ToPeer) : ISnapshot;

[EnableAutoGeneration]
public class PollAggregate : SnapshotAggregateRoot<PollAggregate, PollId, PollSnapshot>
{
    private readonly PollState _state = new();

    public PollAggregate(PollId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void UpdatePollVoters(List<PollAnswerVoter> answerVoters)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new PollVotersUpdatedEvent(_state.PollId, answerVoters));
    }

    public void DeletePollAnswer(RequestInfo requestInfo, Peer peer, string option, int messageId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var answers = _state.Answers2;
        IPollAnswer? pollAnswer = null;
        foreach (var item in answers)
        {
            if (item is TPollAnswer pollAnswer1)
            {
                if (pollAnswer1.Option.Equals(option, StringComparison.OrdinalIgnoreCase))
                {
                    if (pollAnswer1.AddedBy != null)
                    {
                        pollAnswer = pollAnswer1;
                        break;
                    }
                }
            }
        }

        if (pollAnswer == null)
        {
            RpcErrors.RpcErrors400.PollOptionInvalid.ThrowRpcError();
        }

        if (pollAnswer != null)
        {
            answers.Remove(pollAnswer);
        }

        Emit(new PollAnswerDeletedEvent(requestInfo, peer, _state.PollId, answers, pollAnswer, messageId, option));
    }

    public void AddPollAnswer(RequestInfo requestInfo, Peer peer, IPollAnswer pollAnswer, int messageId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

        var answers = _state.Answers2;
        if (pollAnswer is TPollAnswer pollAnswer1)
        {
            pollAnswer1.Option = $"{answers.Count}";
        }
        answers.Add(pollAnswer);

        Emit(new PollAnswerAddedEvent(requestInfo, peer, _state.PollId, answers, pollAnswer, messageId));
    }

    public void Vote(RequestInfo requestInfo, long voteUserPeerId, IReadOnlyCollection<string> options)
    {
        var maxRecentVoters = 3;
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
            if (_state.RevotingDisabled)
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
        if (answerVoters.Count == 0)
        {
            foreach (var pollAnswer in _state.Answers2)
            {
                if (pollAnswer is TPollAnswer pollAnswer1)
                {
                    answerVoters.Add(new PollAnswerVoter(false, pollAnswer1.Option, 0, []));
                }
            }
        }

        List<string>? retractVoteOptions = null;
        if (options.Count == 0)
        {
            retractVoteOptions = _state.GetVoteOptionsByUserId(voteUserPeerId);
            foreach (var pollAnswerVoter in answerVoters)
            {
                if (retractVoteOptions.Contains(pollAnswerVoter.Option))
                {
                    pollAnswerVoter.Voters--;
                    pollAnswerVoter.RecentVoters.Remove(voteUserPeerId);
                }
            }
        }
        else
        {
            foreach (var answerVoter in answerVoters)
            {
                if (options.Contains(answerVoter.Option))
                {
                    if (answerVoter.RecentVoters.Contains(voteUserPeerId))
                    {
                        answerVoter.RecentVoters.Remove(voteUserPeerId);
                    }

                    answerVoter.RecentVoters.Add(voteUserPeerId);
                    answerVoter.Voters++;

                    if (answerVoter.RecentVoters.Count > maxRecentVoters)
                    {
                        answerVoter.RecentVoters.RemoveAt(0);
                    }
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
        long creatorUserId)
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

    public void CreatePoll2(Peer toPeer,
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
        long creatorUserId,
        int? closeDate,
        int? closePeriod,
        bool openAnswers,
        bool revotingDisabled,
        bool shuffleAnswers,
        bool hideResultsUntilClose,
        IMessageMedia? attachedMedia,
        IMessageMedia? solutionMedia,
        List<IPollAnswer> answers2)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        if (answers.Count > MyTelegramConsts.MaxVoteOptions || answers2.Count > MyTelegramConsts.MaxVoteOptions)
        {
            RpcErrors.RpcErrors400.OptionsTooMuch.ThrowRpcError();
        }

        List<string> options = answers2.Select((_, index) => $"{index}").ToList();
        var date = DateTime.UtcNow.ToTimestamp();

        Emit(new PollCreatedV2Event(toPeer,
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
            creatorUserId,
            closeDate,
            closePeriod,
            openAnswers,
            revotingDisabled,
            shuffleAnswers,
            hideResultsUntilClose,
            attachedMedia,
            solutionMedia,
            answers2,
            options,
            date
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

    protected override Task<PollSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new PollSnapshot(_state.PollId, _state.Options, _state.Answers2, _state.Closed,
            _state.Quiz, _state.MultipleChoice, _state.VotedPeerIds, _state.ToPeer));
    }

    protected override Task LoadSnapshotAsync(PollSnapshot snapshot, ISnapshotMetadata metadata, CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);

        return Task.CompletedTask;
    }
}