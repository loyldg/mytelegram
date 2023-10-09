namespace MyTelegram.ReadModel.Impl;

public class ChatInviteReadModel : IChatInviteReadModel,
    IAmReadModelFor<ChannelAggregate, ChannelId, ChannelInviteExportedEvent>,
    IAmReadModelFor<ChannelAggregate, ChannelId, ChannelInviteEditedEvent>,
    IAmReadModelFor<ChannelAggregate, ChannelId, ChannelInviteDeletedEvent>
{
    public long InviteId { get; private set; }
    public virtual long AdminId { get; private set; }
    public virtual long PeerId { get; private set; }
    public string? Title { get; private set; }
    public bool RequestNeeded { get; private set; }
    public virtual int Date { get; private set; }
    public virtual int? ExpireDate { get; private set; }
    public virtual string Id { get; private set; } = null!;
    public virtual string Link { get; set; } = null!;
    public virtual bool Permanent { get; private set; }
    public virtual bool Revoked { get; private set; }
    public virtual int StartDate { get; private set; }
    public virtual int Usage { get; private set; }
    public virtual int? UsageLimit { get; private set; }
    public virtual int? Requested { get; private set; }


    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
            IDomainEvent<ChannelAggregate, ChannelId, ChannelInviteExportedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = ChatInviteId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.InviteId).Value;

        PeerId = domainEvent.AggregateEvent.ChannelId;
        Link = domainEvent.AggregateEvent.Link;
        Revoked = domainEvent.AggregateEvent.Revoke;
        Permanent = domainEvent.AggregateEvent.Permanent;
        AdminId = domainEvent.AggregateEvent.AdminId;
        Date = domainEvent.AggregateEvent.Date;
        StartDate = domainEvent.AggregateEvent.StartDate;
        ExpireDate = domainEvent.AggregateEvent.ExpireDate;
        UsageLimit = domainEvent.AggregateEvent.UsageLimit;
        Usage = 0;
        Title = domainEvent.AggregateEvent.Title;
        RequestNeeded = domainEvent.AggregateEvent.RequestNeeded;
        InviteId = domainEvent.AggregateEvent.InviteId;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<ChannelAggregate, ChannelId, ChannelInviteEditedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = ChatInviteId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.InviteId).Value;
        Link = domainEvent.AggregateEvent.Link;
        Revoked = domainEvent.AggregateEvent.Revoke;
        ExpireDate = domainEvent.AggregateEvent.ExpireDate;
        UsageLimit = domainEvent.AggregateEvent.UsageLimit;
        Title = domainEvent.AggregateEvent.Title;
        RequestNeeded = domainEvent.AggregateEvent.RequestNeeded;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<ChannelAggregate, ChannelId, ChannelInviteDeletedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = ChatInviteId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.InviteId).Value;
        context.MarkForDeletion();

        return Task.CompletedTask;
    }
}
