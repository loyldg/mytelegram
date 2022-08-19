using MyTelegram.Domain.Aggregates.Poll;
using MyTelegram.Domain.Commands;

namespace MyTelegram.Domain.Commands.Poll;

public class VoteCommand : RequestCommand2<PollAggregate, PollId, IExecutionResult>, IHasCorrelationId
{
    public long VoteUserPeerId { get; }
    public IReadOnlyCollection<string> Options { get; }

    public VoteCommand(PollId aggregateId, RequestInfo request, long voteUserPeerId, IReadOnlyCollection<string> options, Guid correlationId) : base(aggregateId, request)
    {
        VoteUserPeerId = voteUserPeerId;
        Options = options;
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
}