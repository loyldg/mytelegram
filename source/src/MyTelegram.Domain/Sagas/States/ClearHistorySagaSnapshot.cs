namespace MyTelegram.Domain.Sagas.States;

public class ClearHistorySagaSnapshot : ISnapshot
{
    public ClearHistorySagaSnapshot(long reqMsgId,
        long selfAuthKeyId,
        long selfPermAuthKeyId,
        long selfUserId,
        PeerType toPeerType,
        long toPeerId,
        bool revoke,
        string messageActionData,
        long randomId,
        bool needWaitForClearParticipantHistory,
        int nextMaxId,
        int totalCountToBeDelete,
        Dictionary<long, int> peerToPts,
        Dictionary<long, List<int>> ownerToMessageIdList)
    {
        ReqMsgId = reqMsgId;
        SelfAuthKeyId = selfAuthKeyId;
        SelfPermAuthKeyId = selfPermAuthKeyId;
        SelfUserId = selfUserId;
        ToPeerType = toPeerType;
        ToPeerId = toPeerId;
        Revoke = revoke;
        MessageActionData = messageActionData;
        RandomId = randomId;
        NeedWaitForClearParticipantHistory = needWaitForClearParticipantHistory;
        TotalCountToBeDelete = totalCountToBeDelete;
        PeerToPts = peerToPts;
        OwnerToMessageIdList = ownerToMessageIdList;
        NextMaxId = nextMaxId;
    }

    public string MessageActionData { get; }

    public bool NeedWaitForClearParticipantHistory { get; }

    //public int ParticipantHistoryCount { get; }
    //public int DeletedParticipantHistoryCount { get; }
    public int NextMaxId { get; }

    public Dictionary<long, List<int>> OwnerToMessageIdList { get; }

    //public int DeletedCount { get; private set; }
    public Dictionary<long, int> PeerToPts { get; }
    public long RandomId { get; }

    public long ReqMsgId { get; }
    public bool Revoke { get; }
    public long SelfAuthKeyId { get; }
    public long SelfPermAuthKeyId { get; }
    public long SelfUserId { get; }
    public long ToPeerId { get; }
    public PeerType ToPeerType { get; }
    public int TotalCountToBeDelete { get; }
}
