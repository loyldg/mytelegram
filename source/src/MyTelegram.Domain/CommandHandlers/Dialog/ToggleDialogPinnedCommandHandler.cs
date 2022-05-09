namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class ToggleDialogPinnedCommandHandler : CommandHandler<DialogAggregate, DialogId, ToggleDialogPinnedCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        ToggleDialogPinnedCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.TogglePinned(command.ReqMsgId, command.Pinned);
        return Task.CompletedTask;
    }
}
