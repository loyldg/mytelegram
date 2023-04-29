namespace MyTelegram.ReadModel.Impl;

public class DraftReadModel : IDraftReadModel,
    IAmReadModelFor<DialogAggregate, DialogId, DraftSavedEvent>,
    IAmReadModelFor<DialogAggregate, DialogId, DraftClearedEvent>

{
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DialogAggregate, DialogId, DraftClearedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        context.MarkForDeletion();
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DialogAggregate, DialogId, DraftSavedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        OwnerPeerId = domainEvent.AggregateEvent.OwnerPeerId;
        Peer = domainEvent.AggregateEvent.Peer;
        Draft = domainEvent.AggregateEvent.Draft;
        return Task.CompletedTask;
    }

    public virtual Draft Draft { get; protected set; } = null!;
    public virtual string Id { get; private set; } = null!;
    public virtual long OwnerPeerId { get; private set; }
    public virtual Peer Peer { get; protected set; } = null!;
}
