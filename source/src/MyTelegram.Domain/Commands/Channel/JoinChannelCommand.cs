namespace MyTelegram.Domain.Commands.Channel;

public class JoinChannelCommand : RequestCommand2<ChannelMemberAggregate, ChannelMemberId, IExecutionResult>//,
    //IHasCorrelationId
{
    public JoinChannelCommand(ChannelMemberId aggregateId,
        RequestInfo requestInfo,
        long selfUserId,
        long channelId) : base(aggregateId, requestInfo)
    {
        SelfUserId = selfUserId;
        ChannelId = channelId;
    }

    public long ChannelId { get; }
    public long SelfUserId { get; }
}