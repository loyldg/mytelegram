namespace MyTelegram.Domain.Aggregates.Dialog;

public class DialogFilterState : AggregateState<DialogFilterAggregate, DialogFilterId, DialogFilterState>,
    IApply<DialogFilterUpdatedEvent>,
    IApply<DialogFilterDeletedEvent>
{
    public int Id { get; private set; }
    public void Apply(DialogFilterUpdatedEvent aggregateEvent)
    {
        Id = aggregateEvent.Filter.Id;
        //throw new NotImplementedException();
    }

    public void Apply(DialogFilterDeletedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }
}