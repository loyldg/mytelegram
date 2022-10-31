namespace MyTelegram.Domain.Sagas.States;

public class DeleteParticipantHistorySagaState : AggregateState<DeleteParticipantHistorySaga,
        DeleteParticipantHistorySagaId, DeleteParticipantHistorySagaState>,
    IApply<DeleteParticipantHistorySagaStartedEvent>,
    IApply<DeleteParticipantHistoryCompletedEvent>,
    IApply<DeleteParticipantHistoryPtsIncrementedEvent>
{
    public int TotalCount { get; private set; }
    public int DeletedCount { get; private set; }
    public int Pts { get; private set; }
    public RequestInfo RequestInfo { get; private set; } = null!;
    public List<int> MessageIds { get; private set; } = null!;
    public long OwnerPeerId { get; private set; }
    public void Apply(DeleteParticipantHistoryCompletedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(DeleteParticipantHistoryPtsIncrementedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
        DeletedCount++;
        Pts = aggregateEvent.Pts;
    }

    public void Apply(DeleteParticipantHistorySagaStartedEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        MessageIds = aggregateEvent.MessageIds;
        OwnerPeerId = aggregateEvent.OwnerPeerId;
        TotalCount = aggregateEvent.MessageIds.Count;
        //throw new NotImplementedException();
    }
}