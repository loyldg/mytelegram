namespace MyTelegram.Domain.Sagas;

public class ReadChannelHistorySagaSnapshot : ISnapshot
{
    public ReadChannelHistorySagaSnapshot(long reqMsgId,
        long readerUid,
        long channelId,
        Guid correlationId
    )
    {
        ReqMsgId = reqMsgId;
        ReaderUid = readerUid;
        ChannelId = channelId;
        CorrelationId = correlationId;
    }

    public long ChannelId { get; }
    public Guid CorrelationId { get; }
    public long ReaderUid { get; }

    public long ReqMsgId { get; }
}
