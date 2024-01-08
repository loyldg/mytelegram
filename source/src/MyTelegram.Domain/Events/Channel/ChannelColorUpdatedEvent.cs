namespace MyTelegram.Domain.Events.Channel;

public class ChannelColorUpdatedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public long ChannelId { get; }
    public PeerColor Color { get; }
    public long? BackgroundEmojiId { get; }
    public bool ForProfile { get; }

    public ChannelColorUpdatedEvent(RequestInfo requestInfo, long channelId, PeerColor color, long? backgroundEmojiId, bool forProfile) : base(requestInfo)
    {
        ChannelId = channelId;
        Color = color;
        BackgroundEmojiId = backgroundEmojiId;
        ForProfile = forProfile;
    }
}