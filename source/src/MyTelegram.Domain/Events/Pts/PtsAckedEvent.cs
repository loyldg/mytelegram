namespace MyTelegram.Domain.Events.Pts;

public class PtsAckedEvent : AggregateEvent<PtsAggregate, PtsId>
{
    public PtsAckedEvent(long peerId,
        long permAuthKeyId,
        long msgId,
        int pts,
        long globalSeqNo,
        Peer toPeer)
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

public class PtsForAuthKeyIdUpdatedEvent : AggregateEvent<PtsAggregate, PtsId>
{
    public long PeerId { get; }
    public long PermAuthKeyId { get; }
    public int Pts { get; }
    public int ChangedUnreadCount { get; }
    public long GlobalSeqNo { get; }

    public PtsForAuthKeyIdUpdatedEvent(long peerId,
        long permAuthKeyId,
        int pts,
        int changedUnreadCount,
        long globalSeqNo)
    {
        PeerId = peerId;
        PermAuthKeyId = permAuthKeyId;
        Pts = pts;
        ChangedUnreadCount = changedUnreadCount;
        GlobalSeqNo = globalSeqNo;
    }
}

//public class ChannelPtsForUserUpdatedEvent : AggregateEvent<ChannelPtsAggregate, ChannelPtsId>
//{
//    public long UserId { get; }
//    public long ChannelId { get; }
//    public int Pts { get; }
//    public long GlobalSeqNo { get; }

//    public ChannelPtsForUserUpdatedEvent(long userId,long channelId,int pts,long globalSeqNo)
//    {
//        UserId = userId;
//        ChannelId = channelId;
//        Pts = pts;
//        GlobalSeqNo = globalSeqNo;
//    }
//}