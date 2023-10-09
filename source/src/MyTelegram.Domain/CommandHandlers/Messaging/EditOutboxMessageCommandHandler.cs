namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class EditOutboxMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, EditOutboxMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        EditOutboxMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditOutboxMessage(command.RequestInfo,
            command.MessageId,
            command.NewMessage,
            command.EditDate,
            command.Entities,
            command.Media
        );

        return Task.CompletedTask;
    }
}