namespace MyTelegram.Domain.Aggregates.Pts;

public class TempPtsIncrementedEvent : AggregateEvent<TempPtsAggregate, TempPtsId>
{
    public long OwnerPeerId { get; }
    public int NewPts { get; }
    public long PermAuthKeyId { get; }
    public int Date { get; }

    public TempPtsIncrementedEvent(long ownerPeerId, int newPts, long permAuthKeyId, int date)
    {
        OwnerPeerId = ownerPeerId;
        NewPts = newPts;
        PermAuthKeyId = permAuthKeyId;
        Date = date;
    }
}