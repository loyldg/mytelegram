namespace MyTelegram.Domain.Aggregates.Channel;


public class ChannelMemberState : AggregateState<ChannelMemberAggregate, ChannelMemberId, ChannelMemberState>,
    IApply<ChannelMemberCreatedEvent>,
    IApply<ChannelCreatorCreatedEvent>,
    //IApply<ChannelMemberJoinedEvent>,
    IApply<ChannelMemberBannedRightsChangedEvent>,
    IApply<ChannelMemberLeftEvent>,
    IApply<ChannelMemberLeftEvent2>,
    IApply<ChannelAdminEditedEvent2>
{
    public bool Banned { get; private set; }

    public ChatBannedRights? BannedRights { get; private set; }

    public bool Kicked { get; private set; }
    public long KickedBy { get; private set; }
    public bool Left { get; private set; }
    public bool IsBot { get; private set; }
    public bool Broadcast { get; private set; }
    public bool IsAdmin { get; private set; }
    public int Version { get; private set; }
    public string? Rank { get; private set; }
    public int UntilDate { get; private set; }
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
        IsBot = aggregateEvent.IsBot;
        Broadcast = aggregateEvent.IsBroadcast;
        UntilDate = aggregateEvent.UntilDate;

        //BannedRights = null;
    }

    //public void Apply(ChannelMemberJoinedEvent aggregateEvent)
    //{
    //}

    public void Apply(ChannelMemberLeftEvent aggregateEvent)
    {
        Left = true;
    }

    public void LoadSnapshot(ChannelMemberSnapshot snapshot)
    {
        BannedRights = snapshot.BannedRights;
        Kicked = snapshot.Kicked;
        KickedBy = snapshot.KickedBy;
        Left = snapshot.Left;
        Banned = snapshot.Banned;
        IsBot = snapshot.IsBot;
        Broadcast = snapshot.Broadcast;
        UntilDate = snapshot.UntilDate;
    }

    public void Apply(ChannelMemberLeftEvent2 aggregateEvent)
    {
        Left = true;
    }

    public void Apply(ChannelAdminEditedEvent2 aggregateEvent)
    {
        IsAdmin = aggregateEvent.IsAdmin;
    }
}
