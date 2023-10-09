namespace MyTelegram.Domain.Events.Dialog;

public class DialogCreatedEvent : AggregateEvent<DialogAggregate, DialogId>
{
    public DialogCreatedEvent(long ownerId,
        Peer toPeer,
        int channelHistoryMinId,
        int topMessageId,
        //string topMessageBoxId,
        DateTime creationTime)
    {
        OwnerId = ownerId;
        ToPeer = toPeer;
        ChannelHistoryMinId = channelHistoryMinId;
        TopMessageId = topMessageId;
        //TopMessageBoxId = topMessageBoxId;
        CreationTime = creationTime;

    }

    public int ChannelHistoryMinId { get; }

    //public string TopMessageBoxId { get; }
    public DateTime CreationTime { get; }

    public long OwnerId { get; }
    public Peer ToPeer { get; }
    public int TopMessageId { get; }

}