namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class CreateInboxMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, CreateInboxMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        CreateInboxMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CreateInboxMessage(command.RequestInfo, command.InboxMessageItem, command.SenderMessageId);
        return Task.CompletedTask;
    }
}