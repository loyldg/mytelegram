namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class CreateDialogCommandHandler : CommandHandler<DialogAggregate, DialogId, CreateDialogCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        CreateDialogCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.Create(
            command.RequestInfo,
            command.OwnerId,
            command.ToPeer,
            command.ChannelHistoryMinId,
            command.TopMessageId);

        return Task.CompletedTask;
    }
}