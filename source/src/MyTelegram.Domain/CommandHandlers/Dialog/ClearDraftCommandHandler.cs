namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class ClearDraftCommandHandler : CommandHandler<DialogAggregate, DialogId, ClearDraftCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        ClearDraftCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.ClearDraft();
        return Task.CompletedTask;
    }
}
