namespace MyTelegram.Domain.Sagas.Events;

public class ClearPartialHistoryCompletedEvent : RequestAggregateEvent2<DeleteMessageSaga, DeleteMessageSagaId>
{
    public ClearPartialHistoryCompletedEvent(RequestInfo requestInfo,
        long ownerPeerId,
        int pts,
        int ptsCount
    ) : base(requestInfo)
    {
        OwnerPeerId = ownerPeerId;
        Pts = pts;
        PtsCount = ptsCount;
    }

    public long OwnerPeerId { get; }
    public int Pts { get; }
    public int PtsCount { get; }
}
