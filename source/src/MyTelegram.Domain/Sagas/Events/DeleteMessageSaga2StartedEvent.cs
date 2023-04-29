namespace MyTelegram.Domain.Sagas.Events;

public class DeleteMessageSaga2StartedEvent : AggregateEvent<DeleteMessageSaga2, DeleteMessageSaga2Id>
{
    public DeleteMessageSaga2StartedEvent(RequestInfo requestInfo,
        bool revoke,
        IReadOnlyList<int> idList,
        long chatCreatorUserId,
        int chatMemberCount,
        bool isClearHistory,
        PeerType toPeerType,
        Guid correlationId
    )
    {
        RequestInfo = requestInfo;
        Revoke = revoke;
        IdList = idList;
        ChatCreatorUserId = chatCreatorUserId;
        ChatMemberCount = chatMemberCount;
        IsClearHistory = isClearHistory;
        ToPeerType = toPeerType;
        CorrelationId = correlationId;
    }

    public long ChatCreatorUserId { get; }
    public int ChatMemberCount { get; }
    public bool IsClearHistory { get; }
    public PeerType ToPeerType { get; }
    public Guid CorrelationId { get; }
    public IReadOnlyList<int> IdList { get; }
    public RequestInfo RequestInfo { get; }
    public bool Revoke { get; }
}
