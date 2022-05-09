namespace MyTelegram.Domain.Sagas.Events;

public class ClearPartialHistoryCompletedEvent : RequestAggregateEvent<DeleteMessageSaga, DeleteMessageSagaId>
{
    public ClearPartialHistoryCompletedEvent(long reqMsgId,
        long ownerPeerId,
        int pts,
        int ptsCount
    ) : base(reqMsgId)
    {
        OwnerPeerId = ownerPeerId;
        Pts = pts;
        PtsCount = ptsCount;
    }

    public long OwnerPeerId { get; }
    public int Pts { get; }
    public int PtsCount { get; }
}
