namespace MyTelegram.Domain.Events.Channel;

public class SetDiscussionGroupEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public SetDiscussionGroupEvent(RequestInfo requestInfo,
        long broadcastChannelId,
        long? groupChannelId) : base(requestInfo)
    {
        BroadcastChannelId = broadcastChannelId;
        GroupChannelId = groupChannelId;
    }

    public long BroadcastChannelId { get; }
    public long? GroupChannelId { get; }
}
