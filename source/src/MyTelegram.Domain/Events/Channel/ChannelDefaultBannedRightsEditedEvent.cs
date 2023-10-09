namespace MyTelegram.Domain.Events.Channel;

public class ChannelDefaultBannedRightsEditedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public ChannelDefaultBannedRightsEditedEvent(RequestInfo requestInfo,
        long channelId,
        ChatBannedRights defaultBannedRights) : base(requestInfo)
    {
        ChannelId = channelId;
        DefaultBannedRights = defaultBannedRights;
    }

    public long ChannelId { get; }
    public ChatBannedRights DefaultBannedRights { get; }
}
