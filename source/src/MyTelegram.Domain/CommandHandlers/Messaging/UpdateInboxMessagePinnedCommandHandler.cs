namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class
    UpdateInboxMessagePinnedCommandHandler : CommandHandler<MessageAggregate, MessageId,
        UpdateInboxMessagePinnedCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        UpdateInboxMessagePinnedCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.UpdateInboxMessagePinned(command.Pinned,
            command.PmOneSize,
            command.Silent,
            command.Date,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}