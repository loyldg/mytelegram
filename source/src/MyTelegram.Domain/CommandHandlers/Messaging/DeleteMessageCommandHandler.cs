namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class DeleteMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, DeleteMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        DeleteMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.DeleteMessage(command.CorrelationId);
        return Task.CompletedTask;
    }
}