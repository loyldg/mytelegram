namespace MyTelegram.Domain.Events.Dialog;

public class PinnedOrderChangedEvent : AggregateEvent<DialogAggregate, DialogId>
{
    public PinnedOrderChangedEvent(int order)
    {
        Order = order;
    }

    public int Order { get; }
}
