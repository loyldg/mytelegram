namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class
    CreateReadingHistoryCommandHandler : CommandHandler<ReadingHistoryAggregate, ReadingHistoryId,
        CreateReadingHistoryCommand>
{
    public override Task ExecuteAsync(ReadingHistoryAggregate aggregate,
        CreateReadingHistoryCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.Create(command.ReaderPeerId, command.TargetPeerId, command.MessageId, command.Date);
        return Task.CompletedTask;
    }
}