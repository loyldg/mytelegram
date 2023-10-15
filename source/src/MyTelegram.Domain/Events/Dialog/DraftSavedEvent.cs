namespace MyTelegram.Domain.Events.Dialog;

public class DraftSavedEvent : RequestAggregateEvent2<DialogAggregate, DialogId>
{
    public DraftSavedEvent(RequestInfo requestInfo,
        long ownerPeerId,
        Peer peer,
        Draft draft) : base(requestInfo)
    {
        OwnerPeerId = ownerPeerId;
        Peer = peer;
        Draft = draft;
    }

    public Draft Draft { get; }

    public long OwnerPeerId { get; }
    public Peer Peer { get; }
}
