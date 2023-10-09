namespace MyTelegram.Domain.CommandHandlers.Channel;

public class JoinChannelCommandHandler : CommandHandler<ChannelMemberAggregate, ChannelMemberId, JoinChannelCommand>
{
    public override Task ExecuteAsync(ChannelMemberAggregate aggregate,
        JoinChannelCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.Join(command.RequestInfo, command.ChannelId, command.SelfUserId);
        return Task.CompletedTask;
    }
}