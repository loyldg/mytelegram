namespace MyTelegram.Domain.Sagas.Events;

public class ReadChannelHistoryStartedEvent : AggregateEvent<ReadChannelHistorySaga, ReadChannelHistorySagaId>,
    IHasCorrelationId
{
    public ReadChannelHistoryStartedEvent(long reqMsgId,
        long readerUid,
        long channelId,
        Guid correlationId)
    {
        ReqMsgId = reqMsgId;
        ReaderUid = readerUid;
        ChannelId = channelId;
        CorrelationId = correlationId;
    }

    public long ChannelId { get; }
    public long ReaderUid { get; }

    public long ReqMsgId { get; }
    public Guid CorrelationId { get; }
}
