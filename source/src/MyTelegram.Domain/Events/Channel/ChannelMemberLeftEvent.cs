namespace MyTelegram.Domain.Events.Channel;

public class ChannelMemberLeftEvent : RequestAggregateEvent<ChannelMemberAggregate, ChannelMemberId>
{
    public ChannelMemberLeftEvent(long reqMsgId,
        long channelId,
        long memberUid) : base(reqMsgId)
    {
        ChannelId = channelId;
        MemberUid = memberUid;
    }

    public long ChannelId { get; }
    public long MemberUid { get; }
}
