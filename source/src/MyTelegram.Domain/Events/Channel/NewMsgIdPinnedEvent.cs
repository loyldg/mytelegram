namespace MyTelegram.Domain.Events.Channel;

public class NewMsgIdPinnedEvent : AggregateEvent<ChannelAggregate, ChannelId>
{
    public NewMsgIdPinnedEvent(int pinnedMsgId,
        bool pinned)
    {
        PinnedMsgId = pinnedMsgId;
        Pinned = pinned;
    }

    public bool Pinned { get; }

    public int PinnedMsgId { get; }
}
