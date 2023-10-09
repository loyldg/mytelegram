namespace MyTelegram.Domain.Sagas.Events;

public class
    ReadChannelHistoryCompletedEvent : RequestAggregateEvent2<ReadChannelHistorySaga, ReadChannelHistorySagaId>
{
    public ReadChannelHistoryCompletedEvent(RequestInfo requestInfo,
        long channelId,
        long senderPeerId,
        int messageId,
        bool needNotifySender,
        int? topMsgId
    ) : base(requestInfo)
    {
        ChannelId = channelId;
        SenderPeerId = senderPeerId;
        MessageId = messageId;
        NeedNotifySender = needNotifySender;
        TopMsgId = topMsgId;
    }

    public long ChannelId { get; }
    public int MessageId { get; }
    public bool NeedNotifySender { get; }
    public int? TopMsgId { get; }
    public long SenderPeerId { get; }
}