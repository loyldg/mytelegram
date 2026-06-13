namespace MyTelegram.ReadModel.Impl;

public class JoinChannelRequestReadModel : ReadModelBase, IJoinChannelRequestReadModel,
    IAmReadModelFor<JoinChannelAggregate, JoinChannelId, JoinChannelRequestCreatedEvent>,
    IAmReadModelFor<JoinChannelAggregate, JoinChannelId, JoinChannelRequestUpdatedEvent>
{
    public long ChannelId { get; private set; }
    public int Date { get; private set; }
    public virtual string Id { get; private set; } = null!;
    public long? InviteId { get; private set; }
    public bool Approved { get; private set; }
    public bool IsJoinRequestProcessed { get; private set; }
    public long? ProcessedByUserId { get; private set; }
    public long UserId { get; private set; }
    public virtual long? Version { get; set; }
    public Task ApplyAsync(IReadModelContext context, IDomainEvent<JoinChannelAggregate, JoinChannelId, JoinChannelRequestCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        ChannelId = domainEvent.AggregateEvent.ChannelId;
        Date = domainEvent.AggregateEvent.Date;
        InviteId = domainEvent.AggregateEvent.InviteId;
        UserId = domainEvent.AggregateEvent.UserId;
        IsJoinRequestProcessed = false;
        ProcessedByUserId = null;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<JoinChannelAggregate, JoinChannelId, JoinChannelRequestUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Approved = domainEvent.AggregateEvent.Approved;
        IsJoinRequestProcessed = true;
        ProcessedByUserId = domainEvent.AggregateEvent.RequestInfo.UserId;

        return Task.CompletedTask;
    }
}