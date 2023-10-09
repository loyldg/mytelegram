namespace MyTelegram.Domain.Sagas.Events;

public class ReadChannelHistoryStartedEvent : RequestAggregateEvent2<ReadChannelHistorySaga, ReadChannelHistorySagaId>
{
    public ReadChannelHistoryStartedEvent(RequestInfo requestInfo,
        long readerUserId,
        long channelId,
        int? topMsgId) : base(requestInfo)
    {
        ReaderUserId = readerUserId;
        ChannelId = channelId;
        TopMsgId = topMsgId;
    }

    public long ChannelId { get; }
    public int? TopMsgId { get; }
    public long ReaderUserId { get; }
}