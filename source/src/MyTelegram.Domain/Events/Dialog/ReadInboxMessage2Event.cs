namespace MyTelegram.Domain.Events.Dialog;

public class ReadInboxMessage2Event : RequestAggregateEvent2<DialogAggregate, DialogId>
{
    public ReadInboxMessage2Event(RequestInfo requestInfo,
        long readerUserId,
        long ownerPeerId,
        //long globalMessageId,
        int maxMessageId,
        int readCount,
        int unreadCount,
        Peer toPeer
        //bool isOut, 
    ) : base(requestInfo)
    {
        ReaderUserId = readerUserId;
        OwnerPeerId = ownerPeerId;
        //GlobalMessageId = globalMessageId;
        MaxMessageId = maxMessageId;
        ReadCount = readCount;
        UnreadCount = unreadCount;
        ToPeer = toPeer;
        //IsOut = isOut;

    }

    //public long GlobalMessageId { get; }
    public int MaxMessageId { get; }
    public int ReadCount { get; }
    public int UnreadCount { get; }
    public Peer ToPeer { get; }
    public long OwnerPeerId { get; }
    public long ReaderUserId { get; }

    //public bool IsOut { get; }

}