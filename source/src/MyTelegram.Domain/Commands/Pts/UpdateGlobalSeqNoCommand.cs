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

//public class UpdateChannelPtsForUserCommand : Command<ChannelPtsAggregate, ChannelPtsId, IExecutionResult>
//{
//    public long UserId { get; }
//    public long ChannelId { get; }
//    public int Pts { get; }
//    public long GlobalSeqNo { get; }

//    public UpdateChannelPtsForUserCommand(ChannelPtsId aggregateId,long userId,long channelId,int pts,long globalSeqNo) : base(aggregateId)
//    {
//        UserId = userId;
//        ChannelId = channelId;
//        Pts = pts;
//        GlobalSeqNo = globalSeqNo;
//    }
//}