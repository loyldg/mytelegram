namespace MyTelegram.Domain.Aggregates.Dialog;

public class DialogFilterAggregate : AggregateRoot<DialogFilterAggregate, DialogFilterId>
{
    private readonly DialogFilterState _state = new();

    public DialogFilterAggregate(DialogFilterId id) : base(id)
    {
        Register(_state);
    }

    public void DeleteDialogFilter(RequestInfo request)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DialogFilterDeletedEvent(request, _state.Id));
    }

    public void UpdateDialogFilter(RequestInfo requestInfo,
        long ownerUserId,
        DialogFilter filter)
    {
        Emit(new DialogFilterUpdatedEvent(requestInfo, ownerUserId, filter));
    }
}
