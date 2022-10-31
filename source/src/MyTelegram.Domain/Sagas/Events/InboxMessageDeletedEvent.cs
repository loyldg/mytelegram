namespace MyTelegram.Domain.Sagas.Events;

public class InboxMessageDeletedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
{
    public InboxMessageDeletedEvent(long ownerPeerId, int messageId, Guid correlationId)
    {
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
    public int MessageId { get; }
    public long OwnerPeerId { get; }
}