namespace MyTelegram.Domain.Commands.Channel;

public class LeaveChannelCommand : RequestCommand<ChannelMemberAggregate, ChannelMemberId, IExecutionResult>
{
    public LeaveChannelCommand(ChannelMemberId aggregateId,
        long reqMsgId,
        long channelId,
        long memberUid) : base(aggregateId, reqMsgId)
    {
        ChannelId = channelId;
        MemberUid = memberUid;
    }

    public long ChannelId { get; }
    public long MemberUid { get; }
}
