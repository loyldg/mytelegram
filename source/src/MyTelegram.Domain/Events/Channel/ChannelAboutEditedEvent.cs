namespace MyTelegram.Domain.Events.Channel;

public class ChannelAboutEditedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public ChannelAboutEditedEvent(RequestInfo requestInfo,
        long channelId,
        string? about) : base(requestInfo)
    {
        ChannelId = channelId;
        About = about;
    }

    public long ChannelId { get; }
    public string? About { get; }
}