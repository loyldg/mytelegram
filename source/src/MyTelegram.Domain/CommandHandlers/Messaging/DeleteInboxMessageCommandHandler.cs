namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class DeleteInboxMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, DeleteInboxMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        DeleteInboxMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.DeleteInboxMessage(command.CorrelationId);
        return Task.CompletedTask;
    }
}
