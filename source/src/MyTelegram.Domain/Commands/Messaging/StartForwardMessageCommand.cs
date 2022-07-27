namespace MyTelegram.Domain.Commands.Messaging;

public class StartForwardMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>,
    IHasCorrelationId
{
    public StartForwardMessageCommand(MessageId aggregateId,
        RequestInfo request,
        Peer fromPeer,
        Peer toPeer,
        IReadOnlyList<int> idList,
        IReadOnlyList<long> randomIdList,
        bool forwardFromLinkedChannel,
        Guid correlationId) : base(aggregateId, request)
    {
        FromPeer = fromPeer;
        ToPeer = toPeer;
        IdList = idList;
        RandomIdList = randomIdList;
        ForwardFromLinkedChannel = forwardFromLinkedChannel;
        CorrelationId = correlationId;
    }

    public Peer FromPeer { get; }
    public IReadOnlyList<int> IdList { get; }
    public IReadOnlyList<long> RandomIdList { get; }
    public bool ForwardFromLinkedChannel { get; }

    public Peer ToPeer { get; }
    public Guid CorrelationId { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return CorrelationId.ToByteArray();
    }
}