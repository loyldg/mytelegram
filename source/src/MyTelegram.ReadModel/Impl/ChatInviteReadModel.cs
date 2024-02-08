namespace MyTelegram.ReadModel.Impl;

public class ChatInviteReadModel : IChatInviteReadModel,
//IAmReadModelFor<ChannelAggregate, ChannelId, ChannelInviteExportedEvent>,
//IAmReadModelFor<ChannelAggregate, ChannelId, ChannelInviteEditedEvent>,
//IAmReadModelFor<ChannelAggregate, ChannelId, ChannelInviteDeletedEvent>
IAmReadModelFor<ChatInviteAggregate, ChatInviteId, ChatInviteCreatedEvent>,
IAmReadModelFor<ChatInviteAggregate, ChatInviteId, ChatInviteEditedEvent>,
IAmReadModelFor<ChatInviteAggregate, ChatInviteId, ChatInviteImportedEvent>,
IAmReadModelFor<ChatInviteAggregate, ChatInviteId, ChatInviteDeletedEvent>
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
    public virtual int? StartDate { get; private set; }
    public virtual int? Usage { get; private set; }
    public virtual int? UsageLimit { get; private set; }
    public virtual int? Requested { get; private set; }


    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<ChatInviteAggregate, ChatInviteId, ChatInviteCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        PeerId = domainEvent.AggregateEvent.ChannelId;
        Link = domainEvent.AggregateEvent.Hash;
        Revoked = false;
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

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<ChatInviteAggregate, ChatInviteId, ChatInviteEditedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;

        // Link does not need to be modified, only a new one will be generated 
        //Link = domainEvent.AggregateEvent.Hash;

        Revoked = domainEvent.AggregateEvent.Revoked;
        ExpireDate = domainEvent.AggregateEvent.ExpireDate;
        UsageLimit = domainEvent.AggregateEvent.UsageLimit;
        Title = domainEvent.AggregateEvent.Title;
        RequestNeeded = domainEvent.AggregateEvent.RequestNeeded;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<ChatInviteAggregate, ChatInviteId, ChatInviteImportedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Usage = domainEvent.AggregateEvent.Usage;
        Requested = domainEvent.AggregateEvent.Requested;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<ChatInviteAggregate, ChatInviteId, ChatInviteDeletedEvent> domainEvent, CancellationToken cancellationToken)
    {
        context.MarkForDeletion();

        return Task.CompletedTask;
    }
}
