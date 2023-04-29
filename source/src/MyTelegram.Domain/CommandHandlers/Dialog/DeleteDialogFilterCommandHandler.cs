namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class
    DeleteDialogFilterCommandHandler : CommandHandler<DialogFilterAggregate, DialogFilterId, DeleteDialogFilterCommand>
{
    public override Task ExecuteAsync(DialogFilterAggregate aggregate,
        DeleteDialogFilterCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.DeleteDialogFilter(command.RequestInfo);

        return Task.CompletedTask;
    }
}
