namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class MarkDialogAsUnreadCommandHandler : CommandHandler<DialogAggregate, DialogId, MarkDialogAsUnreadCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        MarkDialogAsUnreadCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.MarkDialogAsUnread(command.Unread);
        return Task.CompletedTask;
    }
}
