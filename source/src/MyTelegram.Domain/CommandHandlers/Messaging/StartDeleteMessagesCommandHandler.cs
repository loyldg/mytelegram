namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class StartDeleteMessagesCommandHandler : CommandHandler<MessageAggregate, MessageId, StartDeleteMessagesCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        StartDeleteMessagesCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.StartDeleteMessages(command.RequestInfo,
            command.Revoke,
            command.IdList,
            command.ChatCreatorId,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
