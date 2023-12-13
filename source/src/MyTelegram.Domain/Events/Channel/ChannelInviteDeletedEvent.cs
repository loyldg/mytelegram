namespace MyTelegram.Domain.Events.Channel;

public class ChannelInviteDeletedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public long ChannelId { get; }
    public long InviteId { get; }

    public ChannelInviteDeletedEvent(RequestInfo requestInfo, long channelId, long inviteId) : base(requestInfo)
    {
        ChannelId = channelId;
        InviteId = inviteId;
    }
}