namespace MyTelegram.Domain.CommandHandlers.Chat;

public class DeleteChatCommandHandler : CommandHandler<ChatAggregate, ChatId, DeleteChatCommand>
{
    public override Task ExecuteAsync(ChatAggregate aggregate,
        DeleteChatCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.DeleteChat(command.RequestInfo);
        return Task.CompletedTask;
    }
}