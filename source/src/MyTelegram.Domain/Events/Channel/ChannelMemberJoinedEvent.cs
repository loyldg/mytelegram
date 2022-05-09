namespace MyTelegram.Domain.Events.Channel;

public class ChannelMemberJoinedEvent : RequestAggregateEvent<ChannelMemberAggregate, ChannelMemberId>,
    IHasCorrelationId
{
    public ChannelMemberJoinedEvent(long reqMsgId,
        long channelId,
        long memberUid,
        int date,
        bool isRejoin,
        Guid correlationId) : base(reqMsgId)
    {
        ChannelId = channelId;
        MemberUid = memberUid;
        Date = date;
        IsRejoin = isRejoin;
        CorrelationId = correlationId;
    }

    public long ChannelId { get; }
    public int Date { get; }
    public bool IsRejoin { get; }
    public long MemberUid { get; }

    public Guid CorrelationId { get; }
}
