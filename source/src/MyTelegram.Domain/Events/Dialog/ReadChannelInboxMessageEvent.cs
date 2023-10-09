namespace MyTelegram.Domain.Events.Dialog;

public class ReadChannelInboxMessageEvent : RequestAggregateEvent2<DialogAggregate, DialogId>
{
    public ReadChannelInboxMessageEvent(RequestInfo requestInfo,
        long readerUserId,
        long channelId,
        int maxId,
        long senderUserId,
        int? topMsgId) : base(requestInfo)
    {
        ReaderUserId = readerUserId;
        ChannelId = channelId;
        MaxId = maxId;
        SenderUserId = senderUserId;
        TopMsgId = topMsgId;
        //MessageBoxId = messageBoxId;
    }

    public long ChannelId { get; }
    public int MaxId { get; }
    public long SenderUserId { get; }
    public int? TopMsgId { get; }

    public long ReaderUserId { get; }
    //public string MessageBoxId { get; }
}