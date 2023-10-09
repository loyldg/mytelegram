namespace MyTelegram.Domain.Sagas.Events;

public class
    ClearSingleUserHistoryCompletedEvent : RequestAggregateEvent2<ClearHistorySaga, ClearHistorySagaId> //, IHasRequestMessageId
{
    public ClearSingleUserHistoryCompletedEvent(RequestInfo requestInfo,
        long selfAuthKeyId,
        int nextMaxId,
        bool isSelf,
        PeerType toPeerType,
        DeletedBoxItem deletedBoxItem) : base(requestInfo)
    {
        SelfAuthKeyId = selfAuthKeyId;
        NextMaxId = nextMaxId;
        IsSelf = isSelf;
        ToPeerType = toPeerType;
        DeletedBoxItem = deletedBoxItem;
    }
    //public long OwnerPeerId { get; }
    //public int Pts { get; }
    //public IReadOnlyList<int> DeletedMessageIdList { get; }

    public DeletedBoxItem DeletedBoxItem { get; }
    public bool IsSelf { get; }
    public int NextMaxId { get; }

    public long SelfAuthKeyId { get; }
    public PeerType ToPeerType { get; }
}