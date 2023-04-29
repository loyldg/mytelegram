namespace MyTelegram.Domain.CommandHandlers.Chat;

public class
    StartDeleteChatMessagesCommandHandler : CommandHandler<ChatAggregate, ChatId, StartDeleteChatMessagesCommand>
{
    public override Task ExecuteAsync(ChatAggregate aggregate,
        StartDeleteChatMessagesCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.StartDeleteChatMessages(command.RequestInfo,
            command.MessageIds,
            command.Revoke,
            command.IsClearHistory,
            command.CorrelationId);

        return Task.CompletedTask;
    }
}
