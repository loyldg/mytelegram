namespace MyTelegram.Domain.Commands.Poll;

public class ClosePollCommand : Command<PollAggregate, PollId, IExecutionResult>
{
    public int CloseDate { get; }

    public ClosePollCommand(PollId aggregateId,int closeDate) : base(aggregateId)
    {
        CloseDate = closeDate;
    }
}
