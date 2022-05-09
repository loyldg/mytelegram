namespace MyTelegram.Domain.Events.Channel;

public class StartSendChannelMessageEvent : /*Request*/AggregateEvent<ChannelAggregate, ChannelId>, IHasCorrelationId
{
    public StartSendChannelMessageEvent( //long reqMsgId,
        long senderPeerId,
        bool senderIsBot,
        bool post,
        int? views,
        int messageId,
        IReadOnlyList<long> botUidList,
        long? linkedChannelId,
        int date,
        Guid correlationId) //: base(/*reqMsgId*/)
    {
        SenderPeerId = senderPeerId;
        SenderIsBot = senderIsBot;
        Post = post;
        Views = views;
        MessageId = messageId;
        BotUidList = botUidList;
        CorrelationId = correlationId;
        LinkedChannelId = linkedChannelId;
        Date = date;
    }

    public IReadOnlyList<long> BotUidList { get; }
    public int Date { get; }
    public long? LinkedChannelId { get; }
    public int MessageId { get; }
    /// <summary>
    /// Whether this is a channel post
    /// </summary>
    public bool Post { get; }
    public bool SenderIsBot { get; }
    public long SenderPeerId { get; }
    public int? Views { get; }

    public Guid CorrelationId { get; }
}
