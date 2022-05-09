namespace MyTelegram.Domain.Events.Channel;

public class PreHistoryHiddenChangedEvent : RequestAggregateEvent<ChannelAggregate, ChannelId>
{
    public PreHistoryHiddenChangedEvent(long reqMsgId,
        long channelId,
        bool hidden
    ) : base(reqMsgId)
    {
        ChannelId = channelId;
        Hidden = hidden;
    }

    public long ChannelId { get; }
    public bool Hidden { get; }
}
