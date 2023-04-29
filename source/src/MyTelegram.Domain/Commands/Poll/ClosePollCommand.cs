namespace MyTelegram.Domain.Commands.Poll;

public class ClosePollCommand : Command<PollAggregate, PollId, IExecutionResult>
{
    public ClosePollCommand(PollId aggregateId,
        int closeDate) : base(aggregateId)
    {
        CloseDate = closeDate;
    }

    public int CloseDate { get; }
}
