namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class
    UpdateOutboxMessagePinnedCommandHandler : CommandHandler<MessageAggregate, MessageId,
        UpdateOutboxMessagePinnedCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        UpdateOutboxMessagePinnedCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.UpdateOutboxMessagePinned(
            command.RequestInfo,
            command.Pinned,
            command.PmOneSize,
            command.Silent,
            command.Date);
        return Task.CompletedTask;
    }
}