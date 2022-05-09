namespace MyTelegram.Domain.Commands.Pts;

public class PtsAckedCommand : Command<PtsAggregate, PtsId, IExecutionResult>
{
    public PtsAckedCommand(PtsId aggregateId,
        long peerId,
        long permAuthKeyId,
        long msgId,
        int pts,
        long globalSeqNo,
        Peer toPeer) : base(aggregateId)
    {
        PeerId = peerId;
        PermAuthKeyId = permAuthKeyId;
        MsgId = msgId;
        Pts = pts;
        GlobalSeqNo = globalSeqNo;
        ToPeer = toPeer;
    }

    public long GlobalSeqNo { get; }
    public long MsgId { get; }
    public long PeerId { get; }
    public long PermAuthKeyId { get; }
    public int Pts { get; }
    public Peer ToPeer { get; }
}

//public class IncrementPtsCommand : Command<PtsAggregate, PtsId, IExecutionResult>
//{
//    public long PeerId { get; }
//    public int Pts { get; }

//    public IncrementPtsCommand(PtsId aggregateId,long peerId,int pts) : base(aggregateId)
//    {
//        PeerId = peerId;
//        Pts = pts;
//    }
//}

//public class IncrementPtsCommand : Command<PtsAggregate, PtsId, IExecutionResult>, IHasCorrelationId
//{
//    public IncrementPtsCommand(PtsId aggregateId,
//        PtsChangeReason reason,
//        Guid correlationId,
//        string messageBoxId = null) : base(aggregateId)
//    {
//        Reason = reason;
//        CorrelationId = correlationId;
//        MessageBoxId = messageBoxId;
//    }
//    public PtsChangeReason Reason { get; }
//    ///// <summary>
//    ///// MessageBoxAggregate不再保存Pts信息，ReadModel在订阅事件时读取Pts的值，然后更新ReadModel(（)reason=OutboxCreated || reason=InboxCreated)
//    ///// </summary>
//    //public string MessageBoxId { get; }
//    public Guid CorrelationId { get; }
//}
