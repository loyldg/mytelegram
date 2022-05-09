namespace MyTelegram.Domain.Events.Messaging;

public class InboxMessageHasReadEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
{
    public long ReqMsgId { get; }
    public long ReaderUid { get; }
    public int MaxMessageId { get; }
    public long SenderPeerId { get; }
    public int SenderMessageId { get; }
    public Peer ToPeer { get; }
    public bool IsOut { get; }
    public Guid CorrelationId { get; }

    public InboxMessageHasReadEvent(long reqMsgId, long readerUid, int maxMessageId,
        long senderPeerId,
        int senderMessageId,
        Peer toPeer,
        bool isOut,
        Guid correlationId
    )
    {
        ReqMsgId = reqMsgId;
        ReaderUid = readerUid;
        MaxMessageId = maxMessageId;
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
        ToPeer = toPeer;
        IsOut = isOut;
        CorrelationId = correlationId;
    }
}