namespace MyTelegram.Domain.Events.Dialog;

public class ReadInboxMessage2Event : RequestAggregateEvent2<DialogAggregate, DialogId>, IHasCorrelationId
{
    public ReadInboxMessage2Event(RequestInfo requestInfo,
        long readerUid,
        long ownerPeerId,
        //long globalMessageId,
        int maxMessageId,
        Peer toPeer,
        //bool isOut,
        Guid correlationId
    ) : base(requestInfo)
    { 
        ReaderUid = readerUid;
        OwnerPeerId = ownerPeerId;
        //GlobalMessageId = globalMessageId;
        MaxMessageId = maxMessageId;
        ToPeer = toPeer;
        //IsOut = isOut;
        CorrelationId = correlationId;
    }

    //public long GlobalMessageId { get; }
    public int MaxMessageId { get; }
    public Peer ToPeer { get; }
    public long OwnerPeerId { get; }
    public long ReaderUid { get; }
     
    //public bool IsOut { get; }
    public Guid CorrelationId { get; }
}
