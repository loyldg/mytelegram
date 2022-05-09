namespace MyTelegram.Domain.Events.Dialog;

public class ReadChannelInboxMessageEvent : RequestAggregateEvent<DialogAggregate, DialogId>, IHasCorrelationId
{
    public ReadChannelInboxMessageEvent(long reqMsgId,
        long readerUid,
        long channelId,
        int maxId,
        //string messageBoxId,
        Guid correlationId) : base(reqMsgId)
    {
        ReaderUid = readerUid;
        ChannelId = channelId;
        MaxId = maxId;
        //MessageBoxId = messageBoxId;
        CorrelationId = correlationId;
    }

    public long ChannelId { get; }
    public int MaxId { get; }

    public long ReaderUid { get; }
    //public string MessageBoxId { get; }

    public Guid CorrelationId { get; }
}
