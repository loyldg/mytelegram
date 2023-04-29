namespace MyTelegram.Domain.Commands.Poll;

public class VoteCommand : RequestCommand2<PollAggregate, PollId, IExecutionResult>, IHasCorrelationId
{
    public VoteCommand(PollId aggregateId,
        RequestInfo requestInfo,
        long voteUserPeerId,
        IReadOnlyCollection<string> options,
        Guid correlationId) : base(aggregateId, requestInfo)
    {
        VoteUserPeerId = voteUserPeerId;
        Options = options;
        CorrelationId = correlationId;
    }

    public long VoteUserPeerId { get; }
    public IReadOnlyCollection<string> Options { get; }

    public Guid CorrelationId { get; }
}
