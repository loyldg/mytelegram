namespace MyTelegram.Domain.Events.Dialog;

public class DialogPinChangedEvent : RequestAggregateEvent<DialogAggregate, DialogId>
{
    public DialogPinChangedEvent(long reqMsgId,
        long ownerPeerId,
        bool pinned) : base(reqMsgId)
    {
        OwnerPeerId = ownerPeerId;
        Pinned = pinned;
    }

    public long OwnerPeerId { get; }
    public bool Pinned { get; }
}
