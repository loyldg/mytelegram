namespace MyTelegram.Domain.Events.Messaging;

public class ForwardMessageStartedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>, IHasCorrelationId
{
    public ForwardMessageStartedEvent(
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
