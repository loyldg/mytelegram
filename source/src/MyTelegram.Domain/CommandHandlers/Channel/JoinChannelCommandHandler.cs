namespace MyTelegram.Domain.CommandHandlers.Channel;

public class JoinChannelCommandHandler : CommandHandler<ChannelMemberAggregate, ChannelMemberId, JoinChannelCommand>
{
    public override Task ExecuteAsync(ChannelMemberAggregate aggregate,
        JoinChannelCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.Join(command.ReqMsgId, command.ChannelId, command.SelfUserId, command.CorrelationId);
        return Task.CompletedTask;
    }
}
