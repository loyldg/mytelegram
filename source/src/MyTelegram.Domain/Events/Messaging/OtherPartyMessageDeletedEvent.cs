namespace MyTelegram.Domain.Events.Messaging;

public class OtherPartyMessageDeletedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
{
    public OtherPartyMessageDeletedEvent(long ownerPeerId,
        int messageId,
        Guid correlationId)
    {
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
        CorrelationId = correlationId;
    }

    public int MessageId { get; }

    public long OwnerPeerId { get; }
    public Guid CorrelationId { get; }
}
