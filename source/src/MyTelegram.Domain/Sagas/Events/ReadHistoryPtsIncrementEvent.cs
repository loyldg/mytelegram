namespace MyTelegram.Domain.Sagas.Events;

public class ReadHistoryPtsIncrementEvent : RequestAggregateEvent2<ReadHistorySaga, ReadHistorySagaId>, IHasCorrelationId
{
    public ReadHistoryPtsIncrementEvent(
        RequestInfo requestInfo,
        long peerId,
        int pts,
        int readCount,
        int unreadCount,
        PtsChangeReason reason) : base(requestInfo)
    {
        PeerId = peerId;
        Pts = pts;
        ReadCount = readCount;
        UnreadCount = unreadCount;
        Reason = reason;
    }

    public long PeerId { get; }
    public int Pts { get; }
    public int ReadCount { get; }
    public int UnreadCount { get; }
    public PtsChangeReason Reason { get; }
}