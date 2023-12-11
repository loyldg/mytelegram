namespace MyTelegram.Domain.Events.Channel;

public class ChannelColorUpdatedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public long ChannelId { get; }
    public PeerColor Color { get; }

    public ChannelColorUpdatedEvent(RequestInfo requestInfo,long channelId, PeerColor color) : base(requestInfo)
    {
        ChannelId = channelId;
        Color = color;
    }
}