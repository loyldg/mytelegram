namespace MyTelegram.Domain.CommandHandlers.Chat;

public class
    EditChatDefaultBannedRightsCommandHandler : CommandHandler<ChatAggregate, ChatId,
        EditChatDefaultBannedRightsCommand>
{
    public override Task ExecuteAsync(ChatAggregate aggregate,
        EditChatDefaultBannedRightsCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditChatDefaultBannedRights(command.RequestInfo, command.ChatBannedRights, command.SelfUserId);
        return Task.CompletedTask;
    }
}