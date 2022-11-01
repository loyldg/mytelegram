namespace MyTelegram.Domain.CommandHandlers.Chat;

public class DeleteChatUserCommandHandler : CommandHandler<ChatAggregate, ChatId, DeleteChatUserCommand>
{
    public override Task ExecuteAsync(ChatAggregate aggregate,
        DeleteChatUserCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.DeleteChatUser(command.RequestInfo,
            command.UserId,
            command.MessageActionData,
            command.RandomId,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
