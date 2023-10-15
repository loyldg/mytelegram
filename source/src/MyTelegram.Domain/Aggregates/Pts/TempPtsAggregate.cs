namespace MyTelegram.Domain.Aggregates.Pts;

public class TempPtsAggregate : AggregateRoot<TempPtsAggregate, TempPtsId>, INotSaveAggregateEvents,
    IApply<TempPtsIncrementedEvent>
{
    public TempPtsAggregate(TempPtsId id) : base(id)
    {
    }

    public void IncrementPts(long ownerPeerId, int newPts, long permAuthKeyId)
    {
        Emit(new TempPtsIncrementedEvent(ownerPeerId, newPts, permAuthKeyId, DateTime.UtcNow.ToTimestamp()));
    }

    public void Apply(TempPtsIncrementedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }
}