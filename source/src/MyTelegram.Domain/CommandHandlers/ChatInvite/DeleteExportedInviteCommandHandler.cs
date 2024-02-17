namespace MyTelegram.Domain.CommandHandlers.ChatInvite;

public class
    DeleteExportedInviteCommandHandler : CommandHandler<ChatInviteAggregate, ChatInviteId, DeleteExportedInviteCommand>
{
    public override Task ExecuteAsync(ChatInviteAggregate aggregate, DeleteExportedInviteCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.DeleteExportedInvite(command.RequestInfo);

        return Task.CompletedTask;
    }
}