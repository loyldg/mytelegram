namespace MyTelegram.Domain.CommandHandlers.Channel;

public class
    UpdateChatInviteRequestPendingCommandHandler : CommandHandler<ChannelAggregate, ChannelId,
        UpdateChatInviteRequestPendingCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate, UpdateChatInviteRequestPendingCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.UpdateChatInviteRequestPending(command.RequestUserId);

        return Task.CompletedTask;
    }
}