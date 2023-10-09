namespace MyTelegram.Domain.Events.Channel;

public class PreHistoryHiddenChangedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public PreHistoryHiddenChangedEvent(RequestInfo requestInfo,
        long channelId,
        bool hidden
    ) : base(requestInfo)
    {
        ChannelId = channelId;
        Hidden = hidden;
    }

    public long ChannelId { get; }
    public bool Hidden { get; }
}

