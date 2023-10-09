namespace MyTelegram.Domain.CommandHandlers.Chat;

public class
    StartClearGroupChatHistoryCommandHandler : CommandHandler<ChatAggregate, ChatId,
        StartClearGroupChatHistoryCommand>
{
    public override Task ExecuteAsync(ChatAggregate aggregate,
        StartClearGroupChatHistoryCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.StartClearGroupChatHistory(command.RequestInfo);
        return Task.CompletedTask;
    }
}