namespace MyTelegram.Domain.Aggregates.Poll;

public class VoteAnswerDeletedEvent : AggregateEvent<PollAggregate, PollId>
{
    public long PollId { get; }
    public long VoterPeerId { get; }

    public VoteAnswerDeletedEvent(long pollId,
        long voterPeerId)
    {
        PollId = pollId;
        VoterPeerId = voterPeerId;
    }
}