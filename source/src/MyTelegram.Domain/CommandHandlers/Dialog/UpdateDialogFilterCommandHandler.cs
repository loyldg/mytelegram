namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class
    UpdateDialogFilterCommandHandler : CommandHandler<DialogFilterAggregate, DialogFilterId, UpdateDialogFilterCommand>
{
    public override Task ExecuteAsync(DialogFilterAggregate aggregate,
        UpdateDialogFilterCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.UpdateDialogFilter(command.Request, command.OwnerUserId, command.Filter);
        return Task.CompletedTask;
    }
}