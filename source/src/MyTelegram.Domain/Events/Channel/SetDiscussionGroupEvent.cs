namespace MyTelegram.Domain.Events.Channel;

public class SetDiscussionGroupEvent : RequestAggregateEvent<ChannelAggregate, ChannelId>
{
    public SetDiscussionGroupEvent(long reqMsgId,
        long broadcastChannelId,
        long? groupChannelId) : base(reqMsgId)
    {
        BroadcastChannelId = broadcastChannelId;
        GroupChannelId = groupChannelId;
    }

    public long BroadcastChannelId { get; }
    public long? GroupChannelId { get; }
}
