namespace MyTelegram.Domain.Sagas.Events;

public class DeleteMessagesCompletedEvent2 : RequestAggregateEvent2<DeleteMessageSaga2, DeleteMessageSaga2Id>
{
    public DeleteMessagesCompletedEvent2(RequestInfo requestInfo,
        PeerType toPeerType,
        DeletedBoxItem selfDeletedBoxItem,
        IReadOnlyList<DeletedBoxItem> deletedBoxItems,
        bool isClearHistory /*, bool isClearHistory, int clearHistoryNextMaxId*/) :
        base(requestInfo)
    {
        //Pts = pts;
        //PtsCount = ptsCount;
        //SelfAuthKeyId = selfAuthKeyId;
        //SelfUserId = selfUserId;
        ToPeerType = toPeerType;
        SelfDeletedBoxItem = selfDeletedBoxItem;
        DeletedBoxItems = deletedBoxItems;
        IsClearHistory = isClearHistory;
        //IsClearHistory = isClearHistory;
        //ClearHistoryNextMaxId = clearHistoryNextMaxId;
    }

    public IReadOnlyList<DeletedBoxItem> DeletedBoxItems { get; }
    public bool IsClearHistory { get; }

    public DeletedBoxItem SelfDeletedBoxItem { get; }

    public PeerType ToPeerType { get; }
    //public bool IsClearHistory { get; }
    //public int ClearHistoryNextMaxId { get; }

    //public int Pts { get; }
    //public int PtsCount { get; }
}
