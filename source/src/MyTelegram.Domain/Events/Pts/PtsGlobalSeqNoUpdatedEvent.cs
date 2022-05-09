namespace MyTelegram.Domain.Events.Pts;

public class PtsGlobalSeqNoUpdatedEvent : AggregateEvent<PtsAggregate, PtsId>
{
    public PtsGlobalSeqNoUpdatedEvent(long peerId,
        long permAuthKeyId,
        long globalSeqNo)
    {
        PeerId = peerId;
        PermAuthKeyId = permAuthKeyId;
        GlobalSeqNo = globalSeqNo;
    }

    public long GlobalSeqNo { get; }
    public long PeerId { get; }
    public long PermAuthKeyId { get; }
}
