namespace MyTelegram.Domain.Sagas.Events;

public class ForwardMessageSagaStartedEvent : RequestAggregateEvent2<ForwardMessageSaga, ForwardMessageSagaId>
{
    public ForwardMessageSagaStartedEvent(
        RequestInfo requestInfo,
        Peer fromPeer,
        Peer toPeer,
        IReadOnlyList<int> idList,
        IReadOnlyList<long> randomIdList,
        bool forwardFromLinkedChannel) : base(requestInfo)
    {
        FromPeer = fromPeer;
        ToPeer = toPeer;
        IdList = idList;
        RandomIdList = randomIdList;
        ForwardFromLinkedChannel = forwardFromLinkedChannel;
    }

    public Peer FromPeer { get; }
    public IReadOnlyList<int> IdList { get; }
    public IReadOnlyList<long> RandomIdList { get; }
    public bool ForwardFromLinkedChannel { get; }

    public Peer ToPeer { get; }
}
