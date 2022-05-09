namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class EditInboxMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, EditInboxMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        EditInboxMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditInboxMessage(command.MessageId,
            command.NewMessage,
            command.EditDate,
            command.Entities,
            command.Media,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}