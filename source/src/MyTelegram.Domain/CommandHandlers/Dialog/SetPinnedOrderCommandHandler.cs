namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class SetPinnedOrderCommandHandler : CommandHandler<DialogAggregate, DialogId, SetPinnedOrderCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        SetPinnedOrderCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.SetPinnedOrder(command.Order);

        return Task.CompletedTask;
    }
}
