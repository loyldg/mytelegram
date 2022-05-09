namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class CreateDialogCommandHandler : CommandHandler<DialogAggregate, DialogId, CreateDialogCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        CreateDialogCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.Create(command.OwnerId,
            command.ToPeer,
            command.ChannelHistoryMinId,
            command.TopMessageId,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
