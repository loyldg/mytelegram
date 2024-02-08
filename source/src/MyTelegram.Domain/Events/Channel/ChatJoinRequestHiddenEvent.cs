namespace MyTelegram.Domain.Events.Channel;

public class ChatJoinRequestHiddenEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public long ChannelId { get; }
    public long UserId { get; }
    public bool Approved { get; }
    public int RequestsPending { get; }
    public List<long> RecentRequesters { get; }

    public ChatJoinRequestHiddenEvent(RequestInfo requestInfo,long channelId,long userId,bool approved,int requestsPending,List<long> recentRequesters) : base(requestInfo)
    {
        ChannelId = channelId;
        UserId = userId;
        Approved = approved;
        RequestsPending = requestsPending;
        RecentRequesters = recentRequesters;
    }
}