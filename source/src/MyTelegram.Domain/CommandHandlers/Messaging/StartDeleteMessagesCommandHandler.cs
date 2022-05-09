namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class StartDeleteMessagesCommandHandler : CommandHandler<MessageAggregate, MessageId, StartDeleteMessagesCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        StartDeleteMessagesCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.StartDeleteMessages(command.Request, command.Revoke, command.IdList, command.CorrelationId);
        return Task.CompletedTask;
    }
}