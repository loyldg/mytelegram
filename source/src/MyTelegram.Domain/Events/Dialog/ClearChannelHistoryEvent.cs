namespace MyTelegram.Domain.Events.Dialog;

public class ClearChannelHistoryEvent : RequestAggregateEvent<DialogAggregate, DialogId>
{
    public ClearChannelHistoryEvent(long reqMsgId,
        int channelHistoryMinId) : base(reqMsgId)
    {
        ChannelHistoryMinId = channelHistoryMinId;
    }

    public int ChannelHistoryMinId { get; }
}
