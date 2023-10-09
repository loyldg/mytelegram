namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class ReadInboxHistoryCommandHandler : CommandHandler<MessageAggregate, MessageId, ReadInboxHistoryCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        ReadInboxHistoryCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.ReadInboxHistory(command.RequestInfo, command.ReaderUserId);
        return Task.CompletedTask;
    }
}