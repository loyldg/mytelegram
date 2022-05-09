namespace MyTelegram.Domain.Commands.Pts;

public class UpdatePtsCommand : Command<PtsAggregate, PtsId, IExecutionResult>
{
    public UpdatePtsCommand(PtsId aggregateId,
        long peerId,
        long permAuthKeyId,
        int newPts
    ) : base(aggregateId)
    {
        NewPts = newPts;
        PeerId = peerId;
        PermAuthKeyId = permAuthKeyId;
    }

    public int NewPts { get; }

    public long PeerId { get; }
    public long PermAuthKeyId { get; }
}
