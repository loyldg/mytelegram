namespace MyTelegram.Domain.Sagas.States;

public class ForwardMessageState : AggregateState<ForwardMessageSaga, ForwardMessageSagaId, ForwardMessageState>,
    IApply<ForwardMessageSagaStartedEvent>,
    IApply<ForwardSingleMessageSuccessEvent>
{
    public Guid CorrelationId { get; private set; }

    public int ForwardCount { get; private set; }
    public Peer FromPeer { get; private set; } = null!;
    public IReadOnlyList<int> IdList { get; private set; } = null!;

    public bool IsCompleted => ForwardCount == IdList.Count;
    public IReadOnlyList<long> RandomIdList { get; private set; } = null!;
    public RequestInfo Request { get; private set; } = default!;
    public Peer ToPeer { get; private set; } = null!;

    public void Apply(ForwardMessageSagaStartedEvent aggregateEvent)
    {
        Request = aggregateEvent.Request;
        FromPeer = aggregateEvent.FromPeer;
        ToPeer = aggregateEvent.ToPeer;
        IdList = aggregateEvent.IdList;
        RandomIdList = aggregateEvent.RandomIdList;
        CorrelationId = aggregateEvent.CorrelationId;
    }

    public void Apply(ForwardSingleMessageSuccessEvent aggregateEvent)
    {
        ForwardCount++;
    }
}
