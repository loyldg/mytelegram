namespace MyTelegram.Domain.CommandHandlers.Chat;

public class
    ReadLatestNoneBotOutboxMessageCommandHandler : CommandHandler<ChatAggregate, ChatId,
        ReadLatestNoneBotOutboxMessageCommand>
{
    public override Task ExecuteAsync(ChatAggregate aggregate,
        ReadLatestNoneBotOutboxMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.ReadLatestNoneBotOutboxMessage(command.SourceCommandId, command.CorrelationId);
        return Task.CompletedTask;
    }
}
