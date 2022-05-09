namespace MyTelegram.Domain.Events.Dialog;

public class DialogCreatedEvent : AggregateEvent<DialogAggregate, DialogId>, IHasCorrelationId
{
    public DialogCreatedEvent(long ownerId,
        Peer toPeer,
        int channelHistoryMinId,
        int topMessageId,
        //string topMessageBoxId,
        DateTime creationTime,
        Guid correlationId)
    {
        OwnerId = ownerId;
        ToPeer = toPeer;
        ChannelHistoryMinId = channelHistoryMinId;
        TopMessageId = topMessageId;
        //TopMessageBoxId = topMessageBoxId;
        CreationTime = creationTime;
        CorrelationId = correlationId;
    }

    public int ChannelHistoryMinId { get; }

    //public string TopMessageBoxId { get; }
    public DateTime CreationTime { get; }

    public long OwnerId { get; }
    public Peer ToPeer { get; }
    public int TopMessageId { get; }
    public Guid CorrelationId { get; }
}
