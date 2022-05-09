namespace MyTelegram.Domain.Events.Channel;

public class SlowModeChangedEvent : RequestAggregateEvent<ChannelAggregate, ChannelId>
{
    public SlowModeChangedEvent(long reqMsgId,
        long channelId,
        int seconds) : base(reqMsgId)
    {
        ChannelId = channelId;
        Seconds = seconds;
    }

    public long ChannelId { get; }
    public int Seconds { get; }
}
