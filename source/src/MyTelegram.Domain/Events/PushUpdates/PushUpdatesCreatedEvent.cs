namespace MyTelegram.Domain.Events.PushUpdates;

public class PushUpdatesCreatedEvent : AggregateEvent<PushUpdatesAggregate, PushUpdatesId>
{
    public PushUpdatesCreatedEvent(
        Peer toPeer,
        long excludeAuthKeyId,
        long excludeUid,
        long onlySendToThisAuthKeyId,
        byte[] data,
        int pts,
        PtsType ptsType,
        long seqNo,
        int date)
    {
        ToPeer = toPeer;
        ExcludeAuthKeyId = excludeAuthKeyId;
        ExcludeUid = excludeUid;
        OnlySendToThisAuthKeyId = onlySendToThisAuthKeyId;
        Data = data;
        Pts = pts;
        PtsType = ptsType;
        SeqNo = seqNo;
        Date = date;
    }

    public byte[] Data { get; }
    public int Date { get; }
    public long ExcludeAuthKeyId { get; }
    public long ExcludeUid { get; }
    public long OnlySendToThisAuthKeyId { get; }
    public int Pts { get; }
    public PtsType PtsType { get; }
    public long SeqNo { get; }

    public Peer ToPeer { get; }
}
