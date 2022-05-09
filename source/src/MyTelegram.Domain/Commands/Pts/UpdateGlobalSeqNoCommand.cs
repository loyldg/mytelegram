namespace MyTelegram.Domain.Commands.Pts;

public class UpdateGlobalSeqNoCommand : Command<PtsAggregate, PtsId, IExecutionResult>
{
    public UpdateGlobalSeqNoCommand(PtsId aggregateId,
        long peerId,
        long permAuthKeyId,
        long globalSeqNo) : base(aggregateId)
    {
        PeerId = peerId;
        PermAuthKeyId = permAuthKeyId;
        GlobalSeqNo = globalSeqNo;
    }

    public long GlobalSeqNo { get; }
    public long PeerId { get; }
    public long PermAuthKeyId { get; }
}
