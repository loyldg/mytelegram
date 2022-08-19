using MyTelegram.Domain.Aggregates.Poll;

namespace MyTelegram.Domain.Commands.Poll;

public class DeleteVoteAnswerCommand : Command<PollAggregate, PollId, IExecutionResult>
{
    public long PollId { get; }
    public long VoterPeerId { get; }

    public DeleteVoteAnswerCommand(PollId aggregateId, long pollId, long voterPeerId) : base(aggregateId)
    {
        PollId = pollId;
        VoterPeerId = voterPeerId;
    }
}