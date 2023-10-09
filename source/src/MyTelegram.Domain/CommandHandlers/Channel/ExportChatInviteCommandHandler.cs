namespace MyTelegram.Domain.CommandHandlers.Channel;

public class ExportChatInviteCommandHandler : CommandHandler<ChannelAggregate, ChannelId, ExportChatInviteCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        ExportChatInviteCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.ExportChatInvite(command.RequestInfo,
            command.AdminId,
            command.InviteId,
            command.Title,
            command.RequestNeeded,
            command.ExpireDate,
            command.UsageLimit,
            //command.LegacyRevokePermanent,
            command.RandomLink,
            command.Date);
        return Task.CompletedTask;
    }
}