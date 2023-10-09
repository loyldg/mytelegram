namespace MyTelegram.Domain.Events.Dialog;

public class OutboxAlreadyReadEvent : RequestAggregateEvent2<DialogAggregate, DialogId>
{
    public OutboxAlreadyReadEvent(
        RequestInfo requestInfo,
        int oldMaxMessageId,
        int newMaxMessageId,
        //long senderMsgId,
        Peer toPeer) : base(requestInfo)
    {

        OldMaxMessageId = oldMaxMessageId;
        NewMaxMessageId = newMaxMessageId;
        ToPeer = toPeer;
        //SenderMsgId = senderMsgId; 
    }

    public int NewMaxMessageId { get; }
    public Peer ToPeer { get; }

    public int OldMaxMessageId { get; }

    //public long SenderMsgId { get; } 


}