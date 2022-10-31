namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class DeleteOutboxMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, DeleteOutboxMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        DeleteOutboxMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.DeleteOutboxMessage(command.CorrelationId);
        return Task.CompletedTask;
    }
}