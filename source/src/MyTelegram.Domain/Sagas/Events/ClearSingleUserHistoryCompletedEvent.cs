namespace MyTelegram.Domain.Sagas.Events;

public class
    ClearSingleUserHistoryCompletedEvent : AggregateEvent<ClearHistorySaga, ClearHistorySagaId> //, IHasRequestMessageId
{
    public ClearSingleUserHistoryCompletedEvent(long reqMsgId,
        long selfAuthKeyId,
        int nextMaxId,
        bool isSelf,
        PeerType toPeerType,
        DeletedBoxItem deletedBoxItem)
    {
        ReqMsgId = reqMsgId;
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

    public long ReqMsgId { get; }
    public long SelfAuthKeyId { get; }
    public PeerType ToPeerType { get; }
}
