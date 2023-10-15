namespace MyTelegram.Domain.Commands.Pts;

public class UpdatePtsForAuthKeyIdCommand : Command<PtsAggregate, PtsId, IExecutionResult>
{
    public long PeerId { get; }
    public long PermAuthKeyId { get; }
    public int NewPts { get; }
    public int ChangedUnreadCount { get; }
    public long GlobalSeqNo { get; }

    public UpdatePtsForAuthKeyIdCommand(PtsId aggregateId, long peerId,
        long permAuthKeyId,
        int newPts,
        int changedUnreadCount,
        long globalSeqNo) : base(aggregateId)
    {
        PeerId = peerId;
        PermAuthKeyId = permAuthKeyId;
        NewPts = newPts;
        ChangedUnreadCount = changedUnreadCount;
        GlobalSeqNo = globalSeqNo;
    }
}