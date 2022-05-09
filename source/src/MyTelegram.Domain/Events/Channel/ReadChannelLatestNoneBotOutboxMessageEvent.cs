namespace MyTelegram.Domain.Events.Channel;

public class ReadChannelLatestNoneBotOutboxMessageEvent : AggregateEvent<ChannelAggregate, ChannelId>, IHasCorrelationId
{
    public ReadChannelLatestNoneBotOutboxMessageEvent(long latestNoneBotSenderPeerId,
        int latestNoneBotSenderMessageId,
        string sourceCommandId,
        Guid correlationId)
    {
        LatestNoneBotSenderPeerId = latestNoneBotSenderPeerId;
        LatestNoneBotSenderMessageId = latestNoneBotSenderMessageId;
        SourceCommandId = sourceCommandId;
        CorrelationId = correlationId;
    }

    public int LatestNoneBotSenderMessageId { get; }

    public long LatestNoneBotSenderPeerId { get; }
    public string SourceCommandId { get; }
    public Guid CorrelationId { get; }
}
