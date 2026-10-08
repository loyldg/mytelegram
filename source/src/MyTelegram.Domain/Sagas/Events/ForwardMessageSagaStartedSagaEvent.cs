namespace MyTelegram.Domain.Sagas.Events;

public class ForwardMessageSagaStartedSagaEvent(
    RequestInfo requestInfo,
    Peer fromPeer,
    Peer toPeer,
    IReadOnlyList<int> idList,
    IReadOnlyList<long> randomIdList,
    bool forwardFromLinkedChannel,
    bool post,
    int? ttlPeriod,
    Dictionary<long, string>? fromNames,
    Peer? sendAs,
    bool dropAuthor,
    bool dropMediaCaptions
    )
    : RequestAggregateEvent2<ForwardMessageSaga, ForwardMessageSagaId>(requestInfo)
{
    //public RequestInfo RequestInfo { get; } = requestInfo;
    public Peer FromPeer { get; } = fromPeer;
    public IReadOnlyList<int> IdList { get; } = idList;
    public IReadOnlyList<long> RandomIdList { get; } = randomIdList;
    public bool ForwardFromLinkedChannel { get; } = forwardFromLinkedChannel;
    public bool Post { get; } = post;
    public int? TtlPeriod { get; } = ttlPeriod;
    public Dictionary<long, string>? FromNames { get; } = fromNames;
    public Peer ToPeer { get; } = toPeer;
    public Peer? SendAs { get; } = sendAs;
    public bool DropAuthor { get; } = dropAuthor;
    public bool DropMediaCaptions { get; } = dropMediaCaptions;
}
