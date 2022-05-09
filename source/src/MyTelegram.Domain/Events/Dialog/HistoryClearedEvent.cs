namespace MyTelegram.Domain.Events.Dialog;

public class HistoryClearedEvent : RequestAggregateEvent2<DialogAggregate, DialogId>, IHasCorrelationId
{
    public HistoryClearedEvent(
        RequestInfo request,
        long ownerPeerId,
        int historyMinId,
        bool revoke,
        Peer toPeer,
        string messageActionData,
        long randomId,
        List<int> messageIdListToBeDelete,
        int nextMaxId,
        Guid correlationId) : base(request)
    {
        OwnerPeerId = ownerPeerId;
        HistoryMinId = historyMinId;
        Revoke = revoke;
        ToPeer = toPeer;
        MessageActionData = messageActionData;
        RandomId = randomId;
        MessageIdListToBeDelete = messageIdListToBeDelete;
        NextMaxId = nextMaxId;
        CorrelationId = correlationId;
    }

    public int HistoryMinId { get; }
    public string MessageActionData { get; }
    public List<int> MessageIdListToBeDelete { get; }
    public int NextMaxId { get; }
    public long OwnerPeerId { get; }
    public long RandomId { get; }
    public bool Revoke { get; }
    public Peer ToPeer { get; }

    public Guid CorrelationId { get; }
}
