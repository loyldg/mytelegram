namespace MyTelegram.Domain.Commands.Dialog;

public class SetPinnedOrderCommand : Command<DialogAggregate, DialogId, IExecutionResult>
{
    public SetPinnedOrderCommand(DialogId aggregateId,
        int order) : base(aggregateId)
    {
        Order = order;
    }

    public int Order { get; }
}
