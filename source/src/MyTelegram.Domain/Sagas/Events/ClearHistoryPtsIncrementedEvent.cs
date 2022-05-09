namespace MyTelegram.Domain.Sagas.Events;

public class ClearHistoryPtsIncrementedEvent : AggregateEvent<ClearHistorySaga, ClearHistorySagaId>
{
    public ClearHistoryPtsIncrementedEvent(long peerId,
        int messageId,
        int pts)
    {
        PeerId = peerId;
        MessageId = messageId;
        Pts = pts;
    }

    public int MessageId { get; }

    public long PeerId { get; }
    public int Pts { get; }
}
