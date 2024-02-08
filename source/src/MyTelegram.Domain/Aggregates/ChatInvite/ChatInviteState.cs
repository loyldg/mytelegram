namespace MyTelegram.Domain.Aggregates.ChatInvite;

public class ChatInviteState : AggregateState<ChatInviteAggregate, ChatInviteId, ChatInviteState>,
    IApply<ChatInviteCreatedEvent>,
    IApply<ChatInviteEditedEvent>,
    IApply<ChatInviteImportedEvent>,
    IApply<ChatInviteDeletedEvent>
{
    public long ChannelId { get; private set; }
    public long InviteId { get; private set; }
    public string Hash { get; private set; } = default!;
    public long AdminId { get; private set; }
    public string? Title { get; private set; }
    public bool RequestNeeded { get; private set; }
    public int? StartDate { get; private set; }
    public int? ExpireDate { get; private set; }
    public int? UsageLimit { get; private set; }
    public bool Permanent { get; private set; }
    public bool Revoked { get; private set; }

    public int? Requested { get; private set; }
    public int? Usage { get; private set; }

    public void Apply(ChatInviteCreatedEvent aggregateEvent)
    {
        ChannelId = aggregateEvent.ChannelId;
        InviteId = aggregateEvent.InviteId;
        Hash = aggregateEvent.Hash;
        AdminId = aggregateEvent.AdminId;
        Title = aggregateEvent.Title;
        RequestNeeded = aggregateEvent.RequestNeeded;
        StartDate = aggregateEvent.StartDate;
        ExpireDate = aggregateEvent.ExpireDate;
        UsageLimit = aggregateEvent.UsageLimit;
        Permanent = aggregateEvent.Permanent;
    }

    public void Apply(ChatInviteEditedEvent aggregateEvent)
    {
        Revoked = aggregateEvent.Revoked;
        Hash = aggregateEvent.Hash;
        Title = aggregateEvent.Title;
        RequestNeeded = aggregateEvent.RequestNeeded;
        StartDate = aggregateEvent.StartDate;
        ExpireDate = aggregateEvent.ExpireDate;
        UsageLimit = aggregateEvent.UsageLimit;
    }

    public void LoadSnapshot(ChatInviteSnapshot snapshot)
    {
        ChannelId = snapshot.ChannelId;
        InviteId = snapshot.InviteId;
        Hash = snapshot.Hash;
        AdminId = snapshot.AdminId;
        Title = snapshot.Title;
        RequestNeeded = snapshot.RequestNeeded;
        StartDate = snapshot.StartDate;
        ExpireDate = snapshot.ExpireDate;
        UsageLimit = snapshot.UsageLimit;
        Permanent = snapshot.Permanent;
        Usage = snapshot.Usage;
        Requested = snapshot.Requested;
    }

    public void Apply(ChatInviteImportedEvent aggregateEvent)
    {
        Usage = aggregateEvent.Usage;
        Requested = aggregateEvent.Requested;
    }

    public void Apply(ChatInviteDeletedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }
}