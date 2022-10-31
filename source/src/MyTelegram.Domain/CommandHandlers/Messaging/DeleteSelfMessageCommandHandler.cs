namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class DeleteSelfMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, DeleteSelfMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        DeleteSelfMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.DeleteSelfMessage(command.MessageId, command.CorrelationId);
        return Task.CompletedTask;
    }
}