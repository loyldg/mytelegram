namespace MyTelegram.Domain.Commands.Poll;

public class VoteCommand : RequestCommand2<PollAggregate, PollId, IExecutionResult>
{
    public long VoteUserPeerId { get; }
    public IReadOnlyCollection<string> Options { get; }

    public VoteCommand(PollId aggregateId, RequestInfo requestInfo, long voteUserPeerId, IReadOnlyCollection<string> options) : base(aggregateId, requestInfo)
    {
        VoteUserPeerId = voteUserPeerId;
        Options = options;
    }
}
