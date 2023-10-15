namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class ClearChannelHistoryCommandHandler : CommandHandler<DialogAggregate, DialogId, ClearChannelHistoryCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        ClearChannelHistoryCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.ClearChannelHistory(command.RequestInfo);
        return Task.CompletedTask;
    }
}
