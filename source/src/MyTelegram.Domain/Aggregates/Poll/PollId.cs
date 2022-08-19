namespace MyTelegram.Domain.Aggregates.Poll;

public class PollId : Identity<PollId>
{
    public PollId(string value) : base(value)
    {
    }

    public static PollId Create(long peerId,
        long pollId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"{peerId}_{pollId}");
    }

    //public static PollId CreatePollIdForVoteAnswer(long pollId,)
}