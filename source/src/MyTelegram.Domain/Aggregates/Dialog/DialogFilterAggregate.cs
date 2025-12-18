namespace MyTelegram.Domain.Aggregates.Dialog;

[EnableAutoGeneration]
public class DialogFilterAggregate : AggregateRoot<DialogFilterAggregate, DialogFilterId>
{
    private readonly DialogFilterState _state = new();
    public DialogFilterAggregate(DialogFilterId id) : base(id)
    {
        Register(_state);
    }

    public void UpdateDialogFilter(RequestInfo requestInfo, long ownerUserId, DialogFilter filter)
    {
        Emit(new DialogFilterUpdatedEvent(requestInfo, ownerUserId, filter));
    }

    public void DeleteDialogFilter(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var filterId = _state.Id;
        Emit(new DialogFilterDeletedEvent(requestInfo, filterId));
    }
}