namespace MyTelegram.Domain.CommandHandlers.Channel;

public class
    LeaveChannelCommandHandler : CommandHandler<ChannelMemberAggregate, ChannelMemberId, LeaveChannelCommand>
{
    public override Task ExecuteAsync(ChannelMemberAggregate aggregate,
        LeaveChannelCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.LeaveChannel(command.ReqMsgId, command.ChannelId, command.MemberUid);
        return Task.CompletedTask;
    }
}
