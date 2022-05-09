namespace MyTelegram.Domain.Sagas.Events;

public class ReadHistoryCompletedEvent : RequestAggregateEvent2<ReadHistorySaga, ReadHistorySagaId>
{
    public ReadHistoryCompletedEvent(RequestInfo request,
        bool senderIsBot,
        long readerUid,
        int readerMessageId,
        int readerPts,
        Peer readerToPeer,
        long senderPeerId,
        int senderPts,
        int senderMessageId,
        bool isOut,
        bool outboxAlreadyRead,
        string sourceCommandId) : base(request)
    {
        SenderIsBot = senderIsBot;
        ReaderUid = readerUid;
        ReaderMessageId = readerMessageId;
        ReaderPts = readerPts;
        ReaderToPeer = readerToPeer;
        SenderPeerId = senderPeerId;
        SenderPts = senderPts;
        SenderMessageId = senderMessageId;
        IsOut = isOut;
        OutboxAlreadyRead = outboxAlreadyRead;
        SourceCommandId = sourceCommandId;
    }

    public bool IsOut { get; }
    public bool OutboxAlreadyRead { get; }
    public int ReaderMessageId { get; }
    public int ReaderPts { get; }
    public Peer ReaderToPeer { get; }
    public long ReaderUid { get; } 
    public bool SenderIsBot { get; }
    public int SenderMessageId { get; }
    public int SenderPts { get; }

    public long SenderPeerId { get; }
    public string SourceCommandId { get; }
}
