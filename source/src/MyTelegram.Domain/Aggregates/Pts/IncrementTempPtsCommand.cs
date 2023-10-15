namespace MyTelegram.Domain.Aggregates.Pts;

public class IncrementTempPtsCommand : Command<TempPtsAggregate, TempPtsId, IExecutionResult>
{
    public long OwnerPeerId { get; }
    public int NewPts { get; }
    public long PermAuthKeyId { get; }

    public IncrementTempPtsCommand(TempPtsId aggregateId, long ownerPeerId, int newPts, long permAuthKeyId = 0) : base(aggregateId)
    {
        OwnerPeerId = ownerPeerId;
        NewPts = newPts;
        PermAuthKeyId = permAuthKeyId;
    }
}