namespace MyTelegram.ReadModel.Impl;

public class ChannelMemberReadModel : IChannelMemberReadModel,
    IAmReadModelFor<ChannelMemberAggregate, ChannelMemberId, ChannelCreatorCreatedEvent>,
    IAmReadModelFor<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent>,
    IAmReadModelFor<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent>,
    IAmReadModelFor<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent>,
    IAmReadModelFor<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent>

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

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        BannedRights = domainEvent.AggregateEvent.BannedRights.ToIntValue();
        UntilDate = domainEvent.AggregateEvent.BannedRights.UntilDate;
        if (domainEvent.AggregateEvent.BannedRights.ViewMessages)
        {
            KickedBy = domainEvent.AggregateEvent.AdminId;
            // Kicked = true;
            Kicked = true;
            Left = true;
        } else
        {
            if (domainEvent.AggregateEvent.NeedRemoveFromKicked)
            {
                Kicked = false;
                KickedBy = 0;
                Left = false;
            }
        }

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
        Kicked = false;
        KickedBy = 0;
        BannedRights = 0;
        UntilDate = 0;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        ChannelId = domainEvent.AggregateEvent.ChannelId;
        UserId = domainEvent.AggregateEvent.MemberUid;
        InviterId = domainEvent.AggregateEvent.MemberUid;
        Date = domainEvent.AggregateEvent.Date;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Left = true;
        return Task.CompletedTask;
    }
}