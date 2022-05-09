namespace MyTelegram.Domain.Events.Dialog;

public class ChannelHistoryClearedEvent : RequestAggregateEvent<DialogAggregate, DialogId>
{
    public ChannelHistoryClearedEvent(long reqMsgId,
        int historyMinId) : base(reqMsgId)
    {
        HistoryMinId = historyMinId;
    }

    //public long OwnerPeerId { get; }
    public int HistoryMinId { get; }
}
