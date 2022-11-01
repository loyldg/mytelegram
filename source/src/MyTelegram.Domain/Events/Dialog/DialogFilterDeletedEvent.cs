namespace MyTelegram.Domain.Aggregates.Dialog;

public class DialogFilterDeletedEvent : RequestAggregateEvent2<DialogFilterAggregate, DialogFilterId>
{
    public int FilterId { get; }

    public DialogFilterDeletedEvent(RequestInfo requestInfo,int filterId) : base(requestInfo)
    {
        FilterId = filterId;
    }
}