namespace MyTelegram.Domain.Events.Dialog;

public class DraftSavedEvent : RequestAggregateEvent<DialogAggregate, DialogId>
{
    public DraftSavedEvent(long reqMsgId,
        long ownerPeerId,
        Peer peer,
        Draft draft) : base(reqMsgId)
    {
        OwnerPeerId = ownerPeerId;
        Peer = peer;
        Draft = draft;
    }

    public Draft Draft { get; }

    public long OwnerPeerId { get; }
    public Peer Peer { get; }
}
