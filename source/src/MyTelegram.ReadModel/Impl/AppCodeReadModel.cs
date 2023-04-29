namespace MyTelegram.ReadModel.Impl;

public class AppCodeReadModel :
    IAppCodeReadModel,
    IAmReadModelFor<AppCodeAggregate, AppCodeId, AppCodeCreatedEvent>
{
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<AppCodeAggregate, AppCodeId, AppCodeCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        AppCodeId = domainEvent.AggregateIdentity.Value;
        PhoneNumber = domainEvent.AggregateEvent.PhoneNumber;
        Code = domainEvent.AggregateEvent.Code;
        Expire = domainEvent.AggregateEvent.Expire;
        PhoneCodeHash = domainEvent.AggregateEvent.PhoneCodeHash;
        CreationTime = domainEvent.AggregateEvent.CreationTime;

        return Task.CompletedTask;
    }

    public virtual string AppCodeId { get; private set; } = null!;
    public virtual string Code { get; private set; } = null!;
    public virtual long CreationTime { get; private set; }
    public virtual int Expire { get; private set; }
    public virtual string Id { get; private set; } = null!;
    public virtual string PhoneCodeHash { get; private set; } = null!;
    public virtual string PhoneNumber { get; private set; } = null!;
}
