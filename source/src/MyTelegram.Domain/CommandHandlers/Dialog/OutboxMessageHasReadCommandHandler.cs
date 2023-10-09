namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class
    OutboxMessageHasReadCommandHandler : CommandHandler<DialogAggregate, DialogId, OutboxMessageHasReadCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        OutboxMessageHasReadCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.OutboxMessageHasRead(command.RequestInfo, command.MaxMessageId, command.OwnerPeerId, command.ToPeer);
        return Task.CompletedTask;
    }
}