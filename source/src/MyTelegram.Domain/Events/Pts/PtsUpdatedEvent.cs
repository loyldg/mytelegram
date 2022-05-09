namespace MyTelegram.Domain.Events.Pts;

public class PtsUpdatedEvent : AggregateEvent<PtsAggregate, PtsId>
{
    public PtsUpdatedEvent(long peerId,
        long permAuthKeyId,
        int newPts)
    {
        PeerId = peerId;
        PermAuthKeyId = permAuthKeyId;
        NewPts = newPts;
    }

    public int NewPts { get; }

    public long PeerId { get; }
    public long PermAuthKeyId { get; }
}
