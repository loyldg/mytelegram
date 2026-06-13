using MyTelegram.Domain.Aggregates.UserConfig;

namespace MyTelegram.ReadModel.Impl;

public class UserConfigReadModel : ReadModelBase, IUserConfigReadModel,
    IAmReadModelFor<UserConfigAggregate, UserConfigId, UserConfigChangedEvent>
{
    public long UserId { get; private set; }
    public string Key { get; private set; } = null!;
    public string Value { get; private set; } = null!;
    public virtual string Id { get; private set; } = null!;
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<UserConfigAggregate, UserConfigId, UserConfigChangedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        UserId = domainEvent.AggregateEvent.UserId;
        Key = domainEvent.AggregateEvent.Key;
        Value = domainEvent.AggregateEvent.Value;

        return Task.CompletedTask;
    }
}