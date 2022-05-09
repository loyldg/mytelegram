namespace MyTelegram.Domain.Events.Dialog;

public class OutboxMessageHasReadEvent : AggregateEvent<DialogAggregate, DialogId>, IHasCorrelationId
{
    public OutboxMessageHasReadEvent(
        long reqMsgId,
        int maxMessageId,
        long ownerPeerId,
        Peer toPeer,
        //bool alreadyRead,
        Guid correlationId)
    {
        ReqMsgId = reqMsgId;
        MaxMessageId = maxMessageId;
        OwnerPeerId = ownerPeerId;
        ToPeer = toPeer;
        //AlreadyRead = alreadyRead;
        CorrelationId = correlationId;
    }

    public int MaxMessageId { get; }
    public long OwnerPeerId { get; }
    public Peer ToPeer { get; }

    public long ReqMsgId { get; }
     

    //public bool AlreadyRead { get; }
    public Guid CorrelationId { get; }
}
