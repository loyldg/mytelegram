//namespace MyTelegram.Domain.Sagas.Events;

//public class DeleteMessagesSagaStartedEvent : RequestAggregateEvent2<DeleteMessageSaga, DeleteMessageSagaId>
//{
//    public DeleteMessagesSagaStartedEvent(
//        RequestInfo request,
//        bool revoke,
//        IReadOnlyList<int> idList,
//        Peer toPeer,
//        bool isClearHistory,
//        int clearHistoryNextMaxId,
//        long randomId,
//        string? messageActionData):base(request)
//    { 
//        Revoke = revoke;
//        IdList = idList;
//        ToPeer = toPeer;
//        IsClearHistory = isClearHistory;
//        ClearHistoryNextMaxId = clearHistoryNextMaxId;
//        RandomId = randomId;
//        MessageActionData = messageActionData;
//    }

//    public int ClearHistoryNextMaxId { get; }
//    public IReadOnlyList<int> IdList { get; }
//    public Peer ToPeer { get; }
//    public bool IsClearHistory { get; }
//    public string? MessageActionData { get; }
//    public long RandomId { get; }
//    public bool Revoke { get; } 
//}
