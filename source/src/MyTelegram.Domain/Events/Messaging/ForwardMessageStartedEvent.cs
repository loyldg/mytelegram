namespace MyTelegram.Domain.Events.Messaging;

public class ForwardMessageStartedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>, IHasCorrelationId
{
    public ForwardMessageStartedEvent(
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

    public Peer FromPeer { get; }
    public IReadOnlyList<int> IdList { get; }
    public IReadOnlyList<long> RandomIdList { get; }

    public Peer ToPeer { get; }
    public Guid CorrelationId { get; }
}