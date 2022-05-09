namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class SaveDraftCommandHandler : CommandHandler<DialogAggregate, DialogId, SaveDraftCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        SaveDraftCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.SaveDraft(command.ReqMsgId,
            command.Message,
            command.NoWebpage,
            command.ReplyToMsgId,
            command.Date,
            command.Entities);
        return Task.CompletedTask;
    }
}
