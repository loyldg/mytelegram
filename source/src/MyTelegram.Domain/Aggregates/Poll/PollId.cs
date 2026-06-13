namespace MyTelegram.Domain.Aggregates.Poll;

public class PollId(string value) : Identity<PollId>(value)
{
    public static PollId Create(long peerId,
        long pollId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"{peerId}_{pollId}");
    }

    public static PollId Create(long pollId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"pollid-{pollId}");
    }
}