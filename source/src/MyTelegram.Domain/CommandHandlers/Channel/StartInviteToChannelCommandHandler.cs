namespace MyTelegram.Domain.CommandHandlers.Channel;

public class
    StartInviteToChannelCommandHandler : CommandHandler<ChannelAggregate, ChannelId, StartInviteToChannelCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        StartInviteToChannelCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.StartInviteToChannel(command.RequestInfo,
            command.InviterId,
            command.MemberUidList,
            command.BotUidList,
            command.Date,
            command.RandomId,
            command.MessageActionData,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
