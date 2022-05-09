namespace MyTelegram.Domain.Commands.PushUpdates;

public class CreatePushUpdatesCommand : Command<PushUpdatesAggregate, PushUpdatesId, IExecutionResult>
{
    public CreatePushUpdatesCommand(PushUpdatesId aggregateId,
        //long selfPermAuthKeyId,
        //PeerType peerType,
        //long peerId,
        //PeerType toPeerType,
        Peer toPeer,
        long excludeAuthKeyId,
        long excludeUid,
        long onlySendToThisAuthKeyId,
        byte[] data,
        int pts,
        PtsType ptsType,
        long seqNo) : base(aggregateId)
    {
        //SelfPermAuthKeyId = selfPermAuthKeyId;
        //ToPeerType = toPeerType;
        //MessageReceiverPeer = messageReceiverPeer;
        ToPeer = toPeer;
        OnlySendToThisAuthKeyId = onlySendToThisAuthKeyId;
        ExcludeAuthKeyId = excludeAuthKeyId;
        ExcludeUid = excludeUid;
        Data = data;
        Pts = pts;
        PtsType = ptsType;
        SeqNo = seqNo;
    }

    public byte[] Data { get; }
    public long ExcludeAuthKeyId { get; }
    public long ExcludeUid { get; }
    public long OnlySendToThisAuthKeyId { get; }
    public int Pts { get; }
    public PtsType PtsType { get; }
    public long SeqNo { get; }

    //public long SelfPermAuthKeyId { get; }
    //public PeerType ToPeerType { get; }
    //public Peer MessageReceiverPeer { get; }
    public Peer ToPeer { get; }
}
