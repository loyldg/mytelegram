namespace MyTelegram.ReadModel.Impl;

public class ImportedContactReadModel : IImportedContactReadModel,
    IAmReadModelFor<ImportedContactAggregate, ImportedContactId, SingleContactImportedEvent>
{
    public virtual string FirstName { get; private set; } = default!;
    public virtual string Id { get; private set; } = null!;
    public virtual string? LastName { get; private set; }
    public virtual string Phone { get; private set; } = null!;
    public virtual long SelfUserId { get; private set; }
    public virtual long TargetUid { get; private set; }
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ImportedContactAggregate, ImportedContactId, SingleContactImportedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        SelfUserId = domainEvent.AggregateEvent.SelfUserId;
        TargetUid = domainEvent.AggregateEvent.PhoneContact.UserId;
        Phone = domainEvent.AggregateEvent.PhoneContact.Phone;
        FirstName = domainEvent.AggregateEvent.PhoneContact.FirstName;
        LastName = domainEvent.AggregateEvent.PhoneContact.LastName;
        return Task.CompletedTask;
    }
}
