namespace MyTelegram.Domain.Events.Dialog;

public class ToggleDialogPinEvent : RequestAggregateEvent<DialogAggregate, DialogId>
{
    public ToggleDialogPinEvent(long reqMsgId,
        bool pinned) : base(reqMsgId)
    {
        Pinned = pinned;
    }

    public bool Pinned { get; }
}
