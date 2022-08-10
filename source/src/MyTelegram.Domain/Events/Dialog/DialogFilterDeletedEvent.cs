namespace MyTelegram.Domain.Aggregates.Dialog;

public class DialogFilterDeletedEvent : RequestAggregateEvent2<DialogFilterAggregate, DialogFilterId>
{
    public int FilterId { get; }

    public DialogFilterDeletedEvent(RequestInfo request,int filterId) : base(request)
    {
        FilterId = filterId;
    }
}