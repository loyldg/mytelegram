namespace MyTelegram.Domain.Events.Channel;

public class ChannelDefaultBannedRightsEditedEvent : RequestAggregateEvent<ChannelAggregate, ChannelId>
{
    public ChannelDefaultBannedRightsEditedEvent(long reqMsgId,
        long channelId,
        ChatBannedRights defaultBannedRights) : base(reqMsgId)
    {
        ChannelId = channelId;
        DefaultBannedRights = defaultBannedRights;
    }

    public long ChannelId { get; }
    public ChatBannedRights DefaultBannedRights { get; }
}
