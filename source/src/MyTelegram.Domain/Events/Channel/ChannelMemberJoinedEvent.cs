namespace MyTelegram.Domain.Events.Channel;

public class ChannelMemberJoinedEvent : RequestAggregateEvent2<ChannelMemberAggregate, ChannelMemberId>
{
    public ChannelMemberJoinedEvent(RequestInfo requestInfo,
        long channelId,
        long memberUserId,
        int date,
        bool isRejoin) : base(requestInfo)
    {
        ChannelId = channelId;
        MemberUserId = memberUserId;
        Date = date;
        IsRejoin = isRejoin;
    }

    public long ChannelId { get; }
    public int Date { get; }
    public bool IsRejoin { get; }
    public long MemberUserId { get; }
}