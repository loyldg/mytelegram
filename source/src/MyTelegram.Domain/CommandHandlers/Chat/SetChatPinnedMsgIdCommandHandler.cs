namespace MyTelegram.Domain.CommandHandlers.Chat;

public class SetChatPinnedMsgIdCommandHandler : CommandHandler<ChatAggregate, ChatId, SetChatPinnedMsgIdCommand>
{
    public override Task ExecuteAsync(ChatAggregate aggregate,
        SetChatPinnedMsgIdCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.SetPinnedMsgId(command.PinnedMsgId);
        return Task.CompletedTask;
    }
}
