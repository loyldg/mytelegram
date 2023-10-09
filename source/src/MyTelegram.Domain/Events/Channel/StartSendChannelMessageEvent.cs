namespace MyTelegram.Domain.Events.Channel;

public class StartSendChannelMessageEvent : /*Request*/RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public StartSendChannelMessageEvent( //long reqMsgId,
        RequestInfo requestInfo,
        long senderPeerId,
        bool senderIsBot,
        bool post,
        int? views,
        int messageId,
        IReadOnlyList<long> botUidList,
        long? linkedChannelId,
        int date) : base(requestInfo)
    {
        SenderPeerId = senderPeerId;
        SenderIsBot = senderIsBot;
        Post = post;
        Views = views;
        MessageId = messageId;
        BotUidList = botUidList;
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
}