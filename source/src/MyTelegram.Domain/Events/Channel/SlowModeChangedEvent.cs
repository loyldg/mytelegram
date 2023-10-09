namespace MyTelegram.Domain.Events.Channel;

public class SlowModeChangedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public long ChannelId { get; }
    public int Seconds { get; }

    public SlowModeChangedEvent(RequestInfo requestInfo, long channelId, int seconds) : base(requestInfo)
    {
        ChannelId = channelId;
        Seconds = seconds;
    }
}
