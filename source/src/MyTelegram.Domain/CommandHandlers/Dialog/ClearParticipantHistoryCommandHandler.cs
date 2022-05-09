namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class
    ClearParticipantHistoryCommandHandler : CommandHandler<DialogAggregate, DialogId,
        ClearParticipantHistoryCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        ClearParticipantHistoryCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.ClearParticipantHistory(command.CorrelationId);
        return Task.CompletedTask;
    }
}
