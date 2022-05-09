namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class
    AddInboxMessageIdToOutboxMessageCommandHandler : CommandHandler<MessageAggregate, MessageId,
        AddInboxMessageIdToOutboxMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        AddInboxMessageIdToOutboxMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.AddInboxMessageIdToOutboxMessage(command.InboxOwnerPeerId, command.InboxMessageId);
        return Task.CompletedTask;
    }
}