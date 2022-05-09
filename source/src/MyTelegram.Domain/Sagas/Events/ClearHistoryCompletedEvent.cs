namespace MyTelegram.Domain.Sagas.Events;

public class ClearHistoryCompletedEvent : AggregateEvent<DeleteMessageSaga, DeleteMessageSagaId>
{
    public ClearHistoryCompletedEvent(
        long reqMsgId,
        DeletedBoxItem selfDeletedBoxItem,
        IReadOnlyList<DeletedBoxItem> deletedBoxItems,
        int nextMaxId)
    {
        ReqMsgId = reqMsgId;
        SelfDeletedBoxItem = selfDeletedBoxItem;
        DeletedBoxItems = deletedBoxItems;
        NextMaxId = nextMaxId;
    }

    public IReadOnlyList<DeletedBoxItem> DeletedBoxItems { get; }
    public int NextMaxId { get; }
    public long ReqMsgId { get; }
    public DeletedBoxItem SelfDeletedBoxItem { get; }
}
