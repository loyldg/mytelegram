namespace MyTelegram.Domain.Events.Channel;

public class ChatInviteRequestPendingUpdatedEvent : AggregateEvent<ChannelAggregate, ChannelId>
{
    public long ChannelId { get; }
    public List<long> ChannelAdmins { get; }
    public List<long> RecentRequesters { get; }
    public int? RequestsPending { get; }

    public ChatInviteRequestPendingUpdatedEvent(long channelId, List<long> channelAdmins, List<long> recentRequesters, int? requestsPending)
    {
        ChannelId = channelId;
        ChannelAdmins = channelAdmins;
        RecentRequesters = recentRequesters;
        RequestsPending = requestsPending;
    }
}