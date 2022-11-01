namespace MyTelegram.Domain.Sagas;

public class ReadHistorySagaSnapshot : ISnapshot
{
    public ReadHistorySagaSnapshot(RequestInfo requestInfo,
        long readerUid,
        int readerMessageId,
        int readerPts,
        bool senderIsBot,
        long senderPeerId,
        int senderPts,
        int senderMessageId,
        Peer readerToPeer,
        bool isOut,
        bool readHistoryCompleted,
        bool outboxPtsIncremented,
        bool inboxPtsIncremented,
        bool latestNoneBotOutboxHasRead,
        bool needReadLatestNoneBotOutboxMessage,
        string sourceCommandId,
        Guid correlationId
    )
    {
        RequestInfo = requestInfo;
        ReaderUid = readerUid;
        ReaderMessageId = readerMessageId;
        ReaderPts = readerPts;
        SenderIsBot = senderIsBot;
        SenderPeerId = senderPeerId;
        SenderPts = senderPts;
        SenderMessageId = senderMessageId;
        ReaderToPeer = readerToPeer;
        IsOut = isOut;
        ReadHistoryCompleted = readHistoryCompleted;
        OutboxPtsIncremented = outboxPtsIncremented;
        InboxPtsIncremented = inboxPtsIncremented;
        LatestNoneBotOutboxHasRead = latestNoneBotOutboxHasRead;
        NeedReadLatestNoneBotOutboxMessage = needReadLatestNoneBotOutboxMessage;
        SourceCommandId = sourceCommandId;
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
    public bool InboxPtsIncremented { get; }
    public bool IsOut { get; }
    public bool LatestNoneBotOutboxHasRead { get; }
    public bool NeedReadLatestNoneBotOutboxMessage { get; }
    public bool OutboxPtsIncremented { get; }
    public int ReaderMessageId { get; }
    public int ReaderPts { get; }

    public Peer ReaderToPeer { get; }

    public RequestInfo RequestInfo { get; }
    public long ReaderUid { get; }
    public bool ReadHistoryCompleted { get; }
    
    public bool SenderIsBot { get; }

    public int SenderMessageId { get; }
    public int SenderPts { get; }

    public long SenderPeerId { get; }
    public string SourceCommandId { get; }
}
