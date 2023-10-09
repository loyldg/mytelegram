namespace MyTelegram.Domain.CommandHandlers.Chat;

public class EditChatAboutCommandHandler : CommandHandler<ChatAggregate, ChatId, EditChatAboutCommand>
{
    public override Task ExecuteAsync(ChatAggregate aggregate,
        EditChatAboutCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditAbout(command.RequestInfo, command.SelfUserId, command.About);
        return Task.CompletedTask;
    }
}