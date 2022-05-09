namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class
    SetDialogPinnedMsgIdCommandHandler : CommandHandler<DialogAggregate, DialogId, SetDialogPinnedMsgIdCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        SetDialogPinnedMsgIdCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.SetPinnedMsgId(command.PinnedMsgId);
        return Task.CompletedTask;
    }
}
