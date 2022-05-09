namespace MyTelegram.Domain.CommandHandlers.Chat;

public class CheckChatStateCommandHandler : CommandHandler<ChatAggregate, ChatId, CheckChatStateCommand>
{
    public override Task ExecuteAsync(ChatAggregate aggregate,
        CheckChatStateCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CheckChatState(command.CorrelationId);
        return Task.CompletedTask;
    }
}