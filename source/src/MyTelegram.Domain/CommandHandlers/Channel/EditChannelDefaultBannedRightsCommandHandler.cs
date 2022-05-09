namespace MyTelegram.Domain.CommandHandlers.Channel;

public class
    EditChannelDefaultBannedRightsCommandHandler : CommandHandler<ChannelAggregate, ChannelId,
        EditChannelDefaultBannedRightsCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        EditChannelDefaultBannedRightsCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditChatDefaultBannedRights(command.ReqMsgId, command.ChatBannedRights, command.SelfUserId);
        return Task.CompletedTask;
    }
}
