namespace MyTelegram.Domain.Commands.Channel;

public class LeaveChannelCommand : RequestCommand2<ChannelMemberAggregate, ChannelMemberId, IExecutionResult>
{
    public LeaveChannelCommand(ChannelMemberId aggregateId,
        RequestInfo requestInfo,
        long channelId,
        long memberUid) : base(aggregateId, requestInfo)
    {
        ChannelId = channelId;
        MemberUid = memberUid;
    }

    public long ChannelId { get; }
    public long MemberUid { get; }
}