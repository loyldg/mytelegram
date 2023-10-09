namespace MyTelegram.Domain.Events.Messaging;

public class InboxMessageHasReadEvent : RequestAggregateEvent2<MessageAggregate, MessageId>
{
    public long ReaderUid { get; }
    public int MaxMessageId { get; }
    public long SenderPeerId { get; }
    public int SenderMessageId { get; }
    public Peer ToPeer { get; }
    public bool IsOut { get; }

    public InboxMessageHasReadEvent(RequestInfo requestInfo, long readerUid, int maxMessageId,
        long senderPeerId,
        int senderMessageId,
        Peer toPeer,
        bool isOut
    ) : base(requestInfo)
    {
        ReaderUid = readerUid;
        MaxMessageId = maxMessageId;
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
        ToPeer = toPeer;
        IsOut = isOut;
    }
}