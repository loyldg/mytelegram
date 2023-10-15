namespace MyTelegram.Domain.Commands.Pts;

public class UpdatePtsCommand : Command<PtsAggregate, PtsId, IExecutionResult>
{
    public UpdatePtsCommand(PtsId aggregateId,
        long peerId,
        long permAuthKeyId,
        int newPts,
        long globalSeqNo,
        int changedUnreadCount
    ) : base(aggregateId)
    {
        NewPts = newPts;
        GlobalSeqNo = globalSeqNo;
        ChangedUnreadCount = changedUnreadCount;
        PeerId = peerId;
        PermAuthKeyId = permAuthKeyId;
    }

    public int NewPts { get; }
    public long GlobalSeqNo { get; }
    public int ChangedUnreadCount { get; }
    public long PeerId { get; }
    public long PermAuthKeyId { get; }
}