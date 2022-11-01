namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class
    StartUpdatePinnedMessageCommandHandler : CommandHandler<MessageAggregate, MessageId,
        StartUpdatePinnedMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        StartUpdatePinnedMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.StartUpdatePinnedMessage(command.RequestInfo,
            command.Pinned,
            command.PmOneSide,
            command.Silent,
            command.Date,
            command.RandomId,
            command.MessageActionData,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}