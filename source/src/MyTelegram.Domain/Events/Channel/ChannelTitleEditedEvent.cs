namespace MyTelegram.Domain.Events.Channel;

public class ChannelTitleEditedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>, IHasCorrelationId
{
    public ChannelTitleEditedEvent(RequestInfo request,
        long channelId,
        string title,
        string messageActionData,
        long randomId,
        Guid correlationId) : base(request)
    {
        ChannelId = channelId;
        Title = title;
        MessageActionData = messageActionData;
        RandomId = randomId;
        CorrelationId = correlationId;
    }

    public long ChannelId { get; }
    public string MessageActionData { get; }
    public long RandomId { get; }
    public string Title { get; }
    public Guid CorrelationId { get; }
}
