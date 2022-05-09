namespace MyTelegram.Domain.CommandHandlers.Chat;

public class EditChatTitleCommandHandler : CommandHandler<ChatAggregate, ChatId, EditChatTitleCommand>
{
    public override Task ExecuteAsync(ChatAggregate aggregate,
        EditChatTitleCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditTitle(command.Request,
            command.Title,
            command.MessageActionData,
            command.RandomId,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
