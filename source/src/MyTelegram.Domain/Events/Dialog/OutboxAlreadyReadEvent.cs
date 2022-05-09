namespace MyTelegram.Domain.Events.Dialog;

public class OutboxAlreadyReadEvent : AggregateEvent<DialogAggregate, DialogId>, IHasCorrelationId
{
    public OutboxAlreadyReadEvent(Guid correlationId,
        int oldMaxMessageId,
        int newMaxMessageId,
        //long senderMsgId,
        Peer toPeer)
    {
        CorrelationId = correlationId;
        OldMaxMessageId = oldMaxMessageId;
        NewMaxMessageId = newMaxMessageId;
        ToPeer = toPeer;
        //SenderMsgId = senderMsgId; 
    }

    public int NewMaxMessageId { get; }
    public Peer ToPeer { get; }

    public int OldMaxMessageId { get; }

    //public long SenderMsgId { get; } 

    public Guid CorrelationId { get; }
}
