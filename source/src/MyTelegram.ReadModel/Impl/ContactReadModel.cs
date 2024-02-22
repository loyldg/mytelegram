using MyTelegram.Domain.Aggregates.Contact;
using MyTelegram.Domain.Events.Contact;

namespace MyTelegram.ReadModel.Impl;

public class ContactReadModel : IContactReadModel,
    IAmReadModelFor<ContactAggregate, ContactId, ContactAddedEvent>,
    IAmReadModelFor<ContactAggregate, ContactId, ContactDeletedEvent>,
    IAmReadModelFor<ContactAggregate,ContactId,ContactProfilePhotoChangedEvent>

{
    public virtual string FirstName { get; private set; } = default!;
    public virtual string Id { get; private set; } = null!;
    public virtual string? LastName { get; private set; }
    public virtual string Phone { get; private set; } = null!;
    public virtual long SelfUserId { get; private set; }
    public virtual long TargetUserId { get; private set; }
    public long? PhotoId { get; private set; }
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ContactAggregate, ContactId, ContactAddedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        SelfUserId = domainEvent.AggregateEvent.SelfUserId;
        TargetUserId = domainEvent.AggregateEvent.TargetUserId;
        Phone = domainEvent.AggregateEvent.Phone;
        FirstName = domainEvent.AggregateEvent.FirstName;
        LastName = domainEvent.AggregateEvent.LastName;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ContactAggregate, ContactId, ContactDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        context.MarkForDeletion();
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<ContactAggregate, ContactId, ContactProfilePhotoChangedEvent> domainEvent, CancellationToken cancellationToken)
    {
        PhotoId= domainEvent.AggregateEvent.PhotoId;

        return Task.CompletedTask;
    }
}
