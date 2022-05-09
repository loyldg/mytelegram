namespace MyTelegram.Domain.Sagas.Events;

public class ReadHistoryPtsIncrementEvent : AggregateEvent<ReadHistorySaga, ReadHistorySagaId>, IHasCorrelationId
{
    public ReadHistoryPtsIncrementEvent(
        long peerId,
        int pts,
        PtsChangeReason reason,
        Guid correlationId)
    {
        PeerId = peerId;
        Pts = pts;
        Reason = reason;
        CorrelationId = correlationId;
    }

    public long PeerId { get; }
    public int Pts { get; }
    public PtsChangeReason Reason { get; }
    public Guid CorrelationId { get; }
}
