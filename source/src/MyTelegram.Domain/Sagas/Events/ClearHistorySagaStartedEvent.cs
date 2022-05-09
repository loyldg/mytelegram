namespace MyTelegram.Domain.Sagas.Events;

public class ClearHistorySagaStartedEvent : RequestAggregateEvent2<ClearHistorySaga, ClearHistorySagaId>
{
    public ClearHistorySagaStartedEvent(
        RequestInfo request,
        long ownerPeerId,
        bool revoke,
        Peer toPeer,
        string messageActionData,
        long randomId,
        int totalCountToBeDelete,
        int nextMaxId):base(request)
    {
        OwnerPeerId = ownerPeerId;
        Revoke = revoke;
        ToPeer = toPeer;
        MessageActionData = messageActionData;
        RandomId = randomId;
        TotalCountToBeDelete = totalCountToBeDelete;
        NextMaxId = nextMaxId;
    }

    public string MessageActionData { get; }
    public int NextMaxId { get; }
    public long OwnerPeerId { get; }
    public long RandomId { get; }
    public bool Revoke { get; }
    public Peer ToPeer { get; }
    public int TotalCountToBeDelete { get; }
}
