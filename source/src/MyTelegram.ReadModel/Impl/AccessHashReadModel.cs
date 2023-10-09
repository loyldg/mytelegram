namespace MyTelegram.ReadModel.Impl;

public class AccessHashReadModel : IAccessHashReadModel,
    IAmReadModelFor<UserAggregate, UserId, UserCreatedEvent>,
    IAmReadModelFor<ChannelAggregate, ChannelId, ChannelCreatedEvent>
{
    public virtual string Id { get; private set; } = null!;

    public long AccessId { get; private set; }
    public long AccessHash { get; private set; }
    public AccessHashType AccessHashType { get; private set; }
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = AccessHashId.Create(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.AccessHash).Value;
        AccessId = domainEvent.AggregateEvent.UserId;
        AccessHash = domainEvent.AggregateEvent.AccessHash;
        AccessHashType = AccessHashType.User;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = AccessHashId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.AccessHash).Value;
        AccessId = domainEvent.AggregateEvent.ChannelId;
        AccessHash = domainEvent.AggregateEvent.AccessHash;
        AccessHashType = AccessHashType.Channel;

        return Task.CompletedTask;
    }
}