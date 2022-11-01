namespace MyTelegram.Domain.Aggregates.Dialog;

public class DialogFilterUpdatedEvent : RequestAggregateEvent2<DialogFilterAggregate, DialogFilterId>
{
    public long OwnerUserId { get; }
    public DialogFilter Filter { get; }
    public DialogFilterUpdatedEvent(RequestInfo requestInfo, long ownerUserId, DialogFilter filter) : base(requestInfo)
    {
        OwnerUserId = ownerUserId;
        Filter = filter;
    }
}