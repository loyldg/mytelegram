namespace MyTelegram.Domain.Aggregates.Dialog;

public class DialogFilterUpdatedEvent : RequestAggregateEvent2<DialogFilterAggregate, DialogFilterId>
{
    public long OwnerUserId { get; }
    public DialogFilter Filter { get; }
    public DialogFilterUpdatedEvent(RequestInfo request, long ownerUserId, DialogFilter filter) : base(request)
    {
        OwnerUserId = ownerUserId;
        Filter = filter;
    }
}