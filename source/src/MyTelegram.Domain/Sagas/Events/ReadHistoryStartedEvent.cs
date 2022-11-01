namespace MyTelegram.Domain.Sagas.Events;

public class ReadHistoryStartedEvent : RequestAggregateEvent2<ReadHistorySaga, ReadHistorySagaId> //,IHasCorrelationId
{
    public ReadHistoryStartedEvent(RequestInfo requestInfo,
        long readerUid,
        int readerMessageId,
        Peer toPeer,
        string sourceCommandId,
        Guid correlationId) : base(requestInfo)
    {
        ReaderUid = readerUid;
        ReaderMessageId = readerMessageId;
        ToPeer = toPeer;
        SourceCommandId = sourceCommandId;
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
    public int ReaderMessageId { get; }
    public long ReaderUid { get; }
    public string SourceCommandId { get; }
    public Peer ToPeer { get; }
}
