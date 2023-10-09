namespace MyTelegram.Domain.Sagas.Events;

public class ReadHistoryOutboxHasReadEvent : RequestAggregateEvent2<ReadHistorySaga, ReadHistorySagaId>, IHasCorrelationId
{
    public ReadHistoryOutboxHasReadEvent(RequestInfo requestInfo, long senderPeerId,
        int senderMessageId) : base(requestInfo)
    {
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
    }

    public int SenderMessageId { get; }

    public long SenderPeerId { get; }
}