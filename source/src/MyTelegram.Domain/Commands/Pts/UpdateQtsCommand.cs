namespace MyTelegram.Domain.Commands.Pts;

public class UpdateQtsCommand : Command<PtsAggregate, PtsId, IExecutionResult>
{
    public UpdateQtsCommand(PtsId aggregateId,
        long peerId,
        int newQts
    ) : base(aggregateId)
    {
        NewQts = newQts;
        PeerId = peerId;
    }

    public int NewQts { get; }

    public long PeerId { get; }
}
