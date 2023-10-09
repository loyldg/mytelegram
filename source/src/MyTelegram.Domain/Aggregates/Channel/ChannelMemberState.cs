namespace MyTelegram.Domain.Aggregates.Channel;

public class ChannelMemberState : AggregateState<ChannelMemberAggregate, ChannelMemberId, ChannelMemberState>,
    IApply<ChannelMemberCreatedEvent>,
    IApply<ChannelCreatorCreatedEvent>,
    IApply<ChannelMemberJoinedEvent>,
    IApply<ChannelMemberBannedRightsChangedEvent>,
    IApply<ChannelMemberLeftEvent>
{
    public bool Banned { get; private set; }

    public ChatBannedRights? BannedRights { get; private set; }

    public bool Kicked { get; private set; }
    public long KickedBy { get; private set; }
    public bool Left { get; private set; }

    public void Apply(ChannelCreatorCreatedEvent aggregateEvent)
    {
    }

    public void Apply(ChannelMemberBannedRightsChangedEvent aggregateEvent)
    {
        BannedRights = aggregateEvent.BannedRights;
        Kicked = aggregateEvent.Kicked;
        KickedBy = aggregateEvent.KickedBy;
        Left = aggregateEvent.Left;
        Banned = aggregateEvent.Banned;
    }

    public void Apply(ChannelMemberCreatedEvent aggregateEvent)
    {
        Kicked = false;
        KickedBy = 0;
        Left = false;
        Banned = false;

        BannedRights = null;
    }

    public void Apply(ChannelMemberJoinedEvent aggregateEvent)
    {
    }

    public void Apply(ChannelMemberLeftEvent aggregateEvent)
    {
        Left = true;
    }
}