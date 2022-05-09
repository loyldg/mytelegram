namespace MyTelegram.Domain.Sagas.Events;

public class ReadHistoryOutboxHasReadEvent : AggregateEvent<ReadHistorySaga, ReadHistorySagaId>, IHasCorrelationId
{
    public ReadHistoryOutboxHasReadEvent(long senderPeerId,
        int senderMessageId,
        Guid correlationId)
    {
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
        CorrelationId = correlationId;
    }

    public int SenderMessageId { get; }

    public long SenderPeerId { get; }
    public Guid CorrelationId { get; }
}
