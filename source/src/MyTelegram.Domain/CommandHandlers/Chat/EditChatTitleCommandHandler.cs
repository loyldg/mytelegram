namespace MyTelegram.Domain.CommandHandlers.Chat;

public class EditChatTitleCommandHandler : CommandHandler<ChatAggregate, ChatId, EditChatTitleCommand>
{
    public override Task ExecuteAsync(ChatAggregate aggregate,
        EditChatTitleCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditTitle(command.RequestInfo,
            command.Title,
            command.MessageActionData,
            command.RandomId);
        return Task.CompletedTask;
    }
}