namespace MyTelegram.Domain.Sagas.Events;

public class OutboxMessageDeletedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
{
    public OutboxMessageDeletedEvent(long ownerPeerId,
        int messageId,
        List<InboxItem>? inboxItems,
        Guid correlationId)
    {
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
        InboxItems = inboxItems;
        CorrelationId = correlationId;
    }

    public List<InboxItem>? InboxItems { get; }
    public int MessageId { get; }
    public long OwnerPeerId { get; }

    public Guid CorrelationId { get; }
}
