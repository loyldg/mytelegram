namespace MyTelegram.Domain.Aggregates.Poll;

public class VoteAnswerCreatedEvent : AggregateEvent<PollAggregate, PollId>
{
    public VoteAnswerCreatedEvent(
        long pollId,
        long voterPeerId,
        string option,
        bool correct)
    {
        PollId = pollId;
        VoterPeerId = voterPeerId;
        Option = option;
        Correct = correct;
    }

    public long PollId { get; }
    public long VoterPeerId { get; }
    public string Option { get; }
    public bool Correct { get; }
}
