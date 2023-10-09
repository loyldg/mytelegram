namespace MyTelegram.Domain.CommandHandlers.Chat;

public class AddChatUserCommandHandler : CommandHandler<ChatAggregate, ChatId, AddChatUserCommand>
{
    public override Task ExecuteAsync(ChatAggregate aggregate,
        AddChatUserCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.AddChatUser(command.RequestInfo,
            command.RequestInfo.UserId,
            command.UserId,
            command.Date,
            command.MessageActionData,
            command.RandomId);
        return Task.CompletedTask;
    }
}