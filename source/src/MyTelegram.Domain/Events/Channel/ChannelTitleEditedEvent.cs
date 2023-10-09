namespace MyTelegram.Domain.Events.Channel;

public class ChannelTitleEditedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public ChannelTitleEditedEvent(RequestInfo requestInfo,
        long channelId,
        string title,
        string messageActionData,
        long randomId) : base(requestInfo)
    {
        ChannelId = channelId;
        Title = title;
        MessageActionData = messageActionData;
        RandomId = randomId;
    }

    public long ChannelId { get; }
    public string MessageActionData { get; }
    public long RandomId { get; }
    public string Title { get; }
}
