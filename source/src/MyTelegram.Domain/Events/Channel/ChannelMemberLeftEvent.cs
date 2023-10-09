namespace MyTelegram.Domain.Events.Channel;

public class ChannelMemberLeftEvent : RequestAggregateEvent2<ChannelMemberAggregate, ChannelMemberId>
{
    public ChannelMemberLeftEvent(RequestInfo requestInfo,
        long channelId,
        long memberUserId) : base(requestInfo)
    {
        ChannelId = channelId;
        MemberUserId = memberUserId;
    }

    public long ChannelId { get; }
    public long MemberUserId { get; }
}