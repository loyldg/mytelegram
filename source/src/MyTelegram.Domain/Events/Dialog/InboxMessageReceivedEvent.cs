namespace MyTelegram.Domain.Events.Dialog;

public class InboxMessageReceivedEvent : AggregateEvent<DialogAggregate, DialogId>, IHasCorrelationId
{
    public InboxMessageReceivedEvent(
        int messageId,
        long ownerPeerId,
        Peer toPeer,
        Guid correlationId
    )
    {
        MessageId = messageId;
        CorrelationId = correlationId;
        OwnerPeerId = ownerPeerId;
        ToPeer = toPeer;
    }

    public int MessageId { get; }
    public long OwnerPeerId { get; }
    public Peer ToPeer { get; }

    public Guid CorrelationId { get; }
}
