namespace MyTelegram.Domain.CommandHandlers.ChatInvite;

public class EditChatInviteCommandHandler : CommandHandler<ChatInviteAggregate, ChatInviteId, EditChatInviteCommand>
{
    public override Task ExecuteAsync(ChatInviteAggregate aggregate, EditChatInviteCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditChatInvite(command.RequestInfo, command.InviteId, command.Hash, command.NewHash,
            command.AdminId, command.Title, command.RequestNeeded, command.StartDate, command.ExpireDate,
            command.UsageLimit, command.Permanent, command.Revoked);

        return Task.CompletedTask;
    }
}