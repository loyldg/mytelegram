namespace MyTelegram.Domain.Commands.Channel;

public class JoinChannelCommand : RequestCommand<ChannelMemberAggregate, ChannelMemberId, IExecutionResult>,
    IHasCorrelationId
{
    public JoinChannelCommand(ChannelMemberId aggregateId,
        long reqMsgId,
        long selfUserId,
        long channelId,
        Guid correlationId) : base(aggregateId, reqMsgId)
    {
        SelfUserId = selfUserId;
        ChannelId = channelId;
        CorrelationId = correlationId;
    }

    public long ChannelId { get; }
    public long SelfUserId { get; }

    public Guid CorrelationId { get; }
}
