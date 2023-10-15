namespace MyTelegram.Domain.Events.Dialog;

public class ChannelHistoryClearedEvent : RequestAggregateEvent2<DialogAggregate, DialogId>
{
    public ChannelHistoryClearedEvent(RequestInfo requestInfo,
        int historyMinId) : base(requestInfo)
    {
        HistoryMinId = historyMinId;
    }

    //public long OwnerPeerId { get; }
    public int HistoryMinId { get; }
}