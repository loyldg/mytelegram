namespace MyTelegram.Domain.Sagas.Events;

public class
    ReadChannelHistoryCompletedEvent : RequestAggregateEvent<ReadChannelHistorySaga, ReadChannelHistorySagaId>
{
    public ReadChannelHistoryCompletedEvent(long reqMsgId,
        long channelId,
        long senderPeerId,
        int messageId,
        bool needNotifySender) : base(reqMsgId)
    {
        ChannelId = channelId;
        SenderPeerId = senderPeerId;
        MessageId = messageId;
        NeedNotifySender = needNotifySender;
    }

    public long ChannelId { get; }
    public int MessageId { get; }
    public bool NeedNotifySender { get; }
    public long SenderPeerId { get; }
}
