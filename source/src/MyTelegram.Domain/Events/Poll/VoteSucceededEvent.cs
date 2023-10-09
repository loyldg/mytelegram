namespace MyTelegram.Domain.Aggregates.Poll;

public class VoteSucceededEvent : RequestAggregateEvent2<PollAggregate, PollId>
{
    public long PollId { get; }
    public long VoteUserPeerId { get; }
    public IReadOnlyCollection<string> Options { get; }
    public IReadOnlyCollection<PollAnswer> Answers { get; }
    public IReadOnlyCollection<string>? CorrectAnswers { get; }
    public IReadOnlyCollection<PollAnswerVoter> AnswerVoters { get; }
    public Peer ToPeer { get; }
    public IReadOnlyCollection<string>? RetractVoteOptions { get; }

    public VoteSucceededEvent(
        RequestInfo requestInfo,
        long pollId,
        long voteUserPeerId,
        IReadOnlyCollection<string> options,
        IReadOnlyCollection<PollAnswer> answers,
        IReadOnlyCollection<string>? correctAnswers,
        IReadOnlyCollection<PollAnswerVoter> answerVoters,
        Peer toPeer,
        IReadOnlyCollection<string>? retractVoteOptions
    ) : base(requestInfo)
    {
        PollId = pollId;
        VoteUserPeerId = voteUserPeerId;
        Options = options;
        Answers = answers;
        CorrectAnswers = correctAnswers;
        ToPeer = toPeer;
        RetractVoteOptions = retractVoteOptions;
        AnswerVoters = answerVoters;

    }


}