namespace MyTelegram.Domain.Events.Dialog;

public class DialogPinChangedEvent : RequestAggregateEvent2<DialogAggregate, DialogId>
{
    public DialogPinChangedEvent(RequestInfo requestInfo,
        long ownerPeerId,
        bool pinned) : base(requestInfo)
    {
        OwnerPeerId = ownerPeerId;
        Pinned = pinned;
    }

    public long OwnerPeerId { get; }
    public bool Pinned { get; }
}