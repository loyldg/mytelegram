namespace MyTelegram.Domain.Commands.Dialog;

public class
    StartDeleteUserMessagesCommandHandler : CommandHandler<DialogAggregate, DialogId, StartDeleteUserMessagesCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        StartDeleteUserMessagesCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.StartDeleteUserMessages(command.RequestInfo, command.Revoke, command.MessageIds, command.IsClearHistory, command.CorrelationId);
        return Task.CompletedTask;
    }
}