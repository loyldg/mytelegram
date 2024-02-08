namespace MyTelegram.Domain.CommandHandlers.ChatInvite;

public class CreateChatInviteCommandHandler : CommandHandler<ChatInviteAggregate, ChatInviteId, CreateChatInviteCommand>
{
    public override Task ExecuteAsync(ChatInviteAggregate aggregate, CreateChatInviteCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CreateChatInvite(command.RequestInfo, command.ChannelId, command.InviteId, command.Hash, command.AdminId, command.Title,
            command.RequestNeeded, command.StartDate, command.ExpireDate, command.UsageLimit, command.Permanent, command.Date);

        return Task.CompletedTask;
    }
}