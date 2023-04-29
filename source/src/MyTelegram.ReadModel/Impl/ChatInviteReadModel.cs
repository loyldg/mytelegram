namespace MyTelegram.ReadModel.Impl;

public class ChatInviteReadModel : IChatInviteReadModel,
    IAmReadModelFor<ChannelAggregate, ChannelId, ExportChatInviteEvent>
{
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChannelAggregate, ChannelId, ExportChatInviteEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;

        ChannelId = domainEvent.AggregateEvent.ChannelId;
        Link = domainEvent.AggregateEvent.Link;
        Revoked = domainEvent.AggregateEvent.Revoke;
        Permanent = domainEvent.AggregateEvent.Permanent;
        AdminId = domainEvent.AggregateEvent.AdminId;
        Date = domainEvent.AggregateEvent.Date;
        StartDate = domainEvent.AggregateEvent.StartDate;
        ExpireDate = domainEvent.AggregateEvent.ExpireDate;
        UsageLimit = domainEvent.AggregateEvent.UsageLimit;
        Usage = 0;
        return Task.CompletedTask;
    }

    public virtual long AdminId { get; private set; }
    public virtual long ChannelId { get; private set; }
    public virtual int Date { get; private set; }
    public virtual int? ExpireDate { get; private set; }
    public virtual string Id { get; private set; } = null!;
    public virtual string Link { get; private set; } = null!;
    public virtual bool Permanent { get; private set; }
    public virtual bool Revoked { get; private set; }
    public virtual int StartDate { get; private set; }
    public virtual int Usage { get; private set; }
    public virtual int? UsageLimit { get; private set; }
}
