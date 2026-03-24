namespace MyTelegram.ReadModel.Impl;

public class ChannelMemberReadModel : IChannelMemberReadModel,
    IAmReadModelFor<ChannelMemberAggregate, ChannelMemberId, ChannelCreatorCreatedEvent>,
    IAmReadModelFor<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent>,
    //IAmReadModelFor<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent>,
    IAmReadModelFor<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent>,
    IAmReadModelFor<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent>,
    IAmReadModelFor<ChannelMemberAggregate, ChannelMemberId, ChannelAdminEditedEvent2>

{
    public int BannedRights { get; private set; }
    public long ChannelId { get; private set; }
    public int Date { get; private set; }
    public virtual string Id { get; private set; } = null!;
    // ReSharper disable once IdentifierTypo
    public long InviterId { get; private set; }
    public bool IsBot { get; private set; }
    public bool Kicked { get; private set; }
    public long KickedBy { get; private set; }
    public bool Left { get; private set; }
    public int UntilDate { get; private set; }
    public long UserId { get; private set; }
    public long? ChatInviteId { get; private set; }
    public ChatJoinType ChatJoinType { get; private set; }
    public int? SubscriptionUntilDate { get; private set; }
    public bool? IsBroadcast { get; private set; }

    public bool IsAdmin { get; private set; }
    //public bool IsCreator { get; private set; }
    public string? Rank { get; private set; }
    public bool CanEdit { get; private set; }
    public long? PromotedBy { get; private set; }
    public int AdminRights { get; private set; }
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelCreatorCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        ChannelId = domainEvent.AggregateEvent.ChannelId;
        UserId = domainEvent.AggregateEvent.UserId;
        InviterId = domainEvent.AggregateEvent.InviterId;
        Date = domainEvent.AggregateEvent.Date;
        ChatJoinType = ChatJoinType.InvitedByAdmin;
        IsBroadcast = domainEvent.AggregateEvent.IsBroadcast;
        IsAdmin = true;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        BannedRights = domainEvent.AggregateEvent.BannedRights.ToIntValue();
        UntilDate = domainEvent.AggregateEvent.BannedRights.UntilDate;
        Kicked = domainEvent.AggregateEvent.Kicked;
        KickedBy = domainEvent.AggregateEvent.KickedBy;
        Left = domainEvent.AggregateEvent.Left;
        IsAdmin = false;
        AdminRights = 0;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        ChannelId = domainEvent.AggregateEvent.ChannelId;
        UserId = domainEvent.AggregateEvent.UserId;
        InviterId = domainEvent.AggregateEvent.InviterId;
        Date = domainEvent.AggregateEvent.Date;
        IsBot = domainEvent.AggregateEvent.IsBot;

        Left = false;
        Kicked = domainEvent.AggregateEvent.Kicked;
        KickedBy = domainEvent.AggregateEvent.KickedBy;
        BannedRights = domainEvent.AggregateEvent.BannedRights.ToIntValue();
        UntilDate = domainEvent.AggregateEvent.UntilDate;

        ChatInviteId = domainEvent.AggregateEvent.ChatInviteId;
        ChatJoinType = domainEvent.AggregateEvent.ChatJoinType;
        IsBroadcast = domainEvent.AggregateEvent.IsBroadcast;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Left = true;

        return Task.CompletedTask;
    }
    public Task ApplyAsync(IReadModelContext context, IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelAdminEditedEvent2> domainEvent, CancellationToken cancellationToken)
    {
        AdminRights = domainEvent.AggregateEvent.AdminRights;
        Rank = domainEvent.AggregateEvent.Rank;
        PromotedBy = domainEvent.AggregateEvent.RequestInfo.UserId;
        IsAdmin = domainEvent.AggregateEvent.IsAdmin;
        if (IsAdmin)
        {
            BannedRights = 0;
        }

        return Task.CompletedTask;
    }
}