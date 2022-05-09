namespace MyTelegram.Domain.Sagas.Events;

public class ForwardMessageSagaStartedEvent : RequestAggregateEvent2<ForwardMessageSaga, ForwardMessageSagaId>
{
    public ForwardMessageSagaStartedEvent(
        RequestInfo request,
        Peer fromPeer,
        Peer toPeer,
        IReadOnlyList<int> idList,
        IReadOnlyList<long> randomIdList,
        Guid correlationId) : base(request)
    {
        FromPeer = fromPeer;
        ToPeer = toPeer;
        IdList = idList;
        RandomIdList = randomIdList;
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
    public Peer FromPeer { get; }
    public IReadOnlyList<int> IdList { get; }
    public IReadOnlyList<long> RandomIdList { get; }

    public Peer ToPeer { get; }
}
