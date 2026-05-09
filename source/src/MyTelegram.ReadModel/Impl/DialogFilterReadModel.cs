namespace MyTelegram.ReadModel.Impl;

public class DialogFilterReadModel : ReadModelBase, IDialogFilterReadModel,
    IAmReadModelFor<DialogFilterAggregate, DialogFilterId, DialogFilterUpdatedEvent>,
    IAmReadModelFor<DialogFilterAggregate, DialogFilterId, DialogFilterDeletedEvent>
{
    public long OwnerUserId { get; private set; }
    public int FolderId { get; private set; }
    public bool IsShareableFolder { get; private set; }
    public DialogFilter Filter { get; private set; } = null!;
    public string? ImportedFromSlug { get; private set; }
    public virtual string Id { get; private set; } = null!;
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DialogFilterAggregate, DialogFilterId, DialogFilterUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        OwnerUserId = domainEvent.AggregateEvent.OwnerUserId;
        FolderId = domainEvent.AggregateEvent.Filter.Id;
        Filter = domainEvent.AggregateEvent.Filter;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DialogFilterAggregate, DialogFilterId, DialogFilterDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        context.MarkForDeletion();

        return Task.CompletedTask;
    }
}
