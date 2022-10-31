namespace MyTelegram.Domain.Sagas.Events;

public class
    DeleteParticipantHistoryCompletedEvent : RequestAggregateEvent2<DeleteParticipantHistorySaga,
        DeleteParticipantHistorySagaId>
{
    public long OwnerPeerId { get; }
    public List<int> MessageIds { get; }
    public int Pts { get; }
    public int PtsCount { get; }
    public int NextMaxId { get; }

    public DeleteParticipantHistoryCompletedEvent(RequestInfo requestInfo,
        long ownerPeerId,
        List<int> messageIds,
        int pts,
        int ptsCount,
        int nextMaxId

    ) : base(requestInfo)
    {
        OwnerPeerId = ownerPeerId;
        MessageIds = messageIds;
        Pts = pts;
        PtsCount = ptsCount;
        NextMaxId = nextMaxId;
    }
}