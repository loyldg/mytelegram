namespace MyTelegram.Domain.Sagas.Events;

public class DeleteMessagesSagaStartedEvent : RequestAggregateEvent2<DeleteMessageSaga, DeleteMessageSagaId>
{
    public DeleteMessagesSagaStartedEvent(
        RequestInfo requestInfo,
        bool revoke,
        IReadOnlyList<int> idList,
        Peer toPeer,
        bool isClearHistory,
        int clearHistoryNextMaxId,
        long randomId,
        string? messageActionData,
        long? chatCreatorId
        ):base(requestInfo)
    { 
        Revoke = revoke;
        IdList = idList;
        ToPeer = toPeer;
        IsClearHistory = isClearHistory;
        ClearHistoryNextMaxId = clearHistoryNextMaxId;
        RandomId = randomId;
        MessageActionData = messageActionData;
        ChatCreatorId = chatCreatorId;
    }

    public int ClearHistoryNextMaxId { get; }
    public IReadOnlyList<int> IdList { get; }
    public Peer ToPeer { get; }
    public bool IsClearHistory { get; }
    public string? MessageActionData { get; }
    public long? ChatCreatorId { get; }
    public long RandomId { get; }
    public bool Revoke { get; } 
}
