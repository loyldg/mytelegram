namespace MyTelegram.Domain.Events.Channel;

public class ChannelAboutEditedEvent : RequestAggregateEvent<ChannelAggregate, ChannelId>
{
    public ChannelAboutEditedEvent(long reqMsgId,
        string? about) : base(reqMsgId)
    {
        About = about;
    }

    public string? About { get; }
}
public class CheckChannelStateCompletedEvent : AggregateEvent<ChannelAggregate, ChannelId>, IHasCorrelationId
{
    public long SenderPeerId { get; }
    public int MessageId { get; }
    public int Date { get; }
    public bool Post { get; }
    public int? Views { get; }
    public IReadOnlyList<long> BotUidList { get; }
    public long? LinkedChannelId { get; }

    public CheckChannelStateCompletedEvent(
        long senderPeerId,
        int messageId,
        int date,
        bool post,
        int? views,
        IReadOnlyList<long> botUidList,
        long? linkedChannelId,
        Guid correlationId)
    {
        SenderPeerId = senderPeerId;
        MessageId = messageId;
        Date = date;
        Post = post;
        Views = views;
        BotUidList = botUidList;
        LinkedChannelId = linkedChannelId;
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
}
