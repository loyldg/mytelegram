namespace MyTelegram.Domain.Events.Dialog;

public class OutboxMessageHasReadEvent : RequestAggregateEvent2<DialogAggregate, DialogId>
{
    public OutboxMessageHasReadEvent(
        RequestInfo requestInfo,
        int maxMessageId,
        long ownerPeerId,
        Peer toPeer) : base(requestInfo)
    {
        MaxMessageId = maxMessageId;
        OwnerPeerId = ownerPeerId;
        ToPeer = toPeer;
        //AlreadyRead = alreadyRead;

    }

    public int MaxMessageId { get; }
    public long OwnerPeerId { get; }
    public Peer ToPeer { get; }


    //public bool AlreadyRead { get; }

}