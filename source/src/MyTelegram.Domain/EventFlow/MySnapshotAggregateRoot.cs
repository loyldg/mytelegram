namespace MyTelegram.Domain.EventFlow;

// taken from https://github.com/eventflow/EventFlow/blob/fix-snapshot-load-sourceids/Source/EventFlow/Snapshots/SnapshotAggregateRoot.cs
public abstract class MySnapshotAggregateRoot<TAggregate, TIdentity, TSnapshot> : AggregateRoot<TAggregate, TIdentity>,
       ISnapshotAggregateRoot<TIdentity, TSnapshot>
       where TAggregate : MySnapshotAggregateRoot<TAggregate, TIdentity, TSnapshot>
       where TIdentity : IIdentity
       where TSnapshot : ISnapshot
{
    protected ISnapshotStrategy SnapshotStrategy { get; }

    protected MySnapshotAggregateRoot(
        TIdentity id,
        ISnapshotStrategy snapshotStrategy)
        : base(id)
    {
        SnapshotStrategy = snapshotStrategy;
    }

    public int? SnapshotVersion { get; private set; }

    public override async Task LoadAsync(
        IEventStore eventStore,
        ISnapshotStore snapshotStore,
        CancellationToken cancellationToken)
    {
        var snapshot = await snapshotStore.LoadSnapshotAsync<TAggregate, TIdentity, TSnapshot>(
            Id,
            cancellationToken)
            ;
        if (snapshot == null)
        {
            await base.LoadAsync(eventStore, snapshotStore, cancellationToken);
            return;
        }

        await LoadSnapshotContainerAsync(snapshot, cancellationToken);

        Version = snapshot.Metadata.AggregateSequenceNumber;
        AddPreviousSourceIds(snapshot.Metadata.PreviousSourceIds);
        var domainEvents = await eventStore.LoadEventsAsync<TAggregate, TIdentity>(
            Id,
            Version + 1,
            cancellationToken)
            ;

        ApplyEvents(domainEvents);
    }

    public override async Task<IReadOnlyCollection<IDomainEvent>> CommitAsync(
        IEventStore eventStore,
        ISnapshotStore snapshotStore,
        ISourceId sourceId,
        CancellationToken cancellationToken)
    {
        var domainEvents = await base.CommitAsync(eventStore, snapshotStore, sourceId, cancellationToken);

        if (!await SnapshotStrategy.ShouldCreateSnapshotAsync(this, cancellationToken).ConfigureAwait(false))
        {
            return domainEvents;
        }

        var snapshotContainer = await CreateSnapshotContainerAsync(sourceId, cancellationToken);
        await snapshotStore.StoreSnapshotAsync<TAggregate, TIdentity, TSnapshot>(
            Id,
            snapshotContainer,
            cancellationToken)
            ;

        return domainEvents;
    }

    private async Task<SnapshotContainer> CreateSnapshotContainerAsync(
        ISourceId sourceId,
        CancellationToken cancellationToken)
    {
        var snapshotTask = CreateSnapshotAsync(cancellationToken);
        var snapshotMetadataTask = CreateSnapshotMetadataAsync(sourceId, cancellationToken);

        await Task.WhenAll(snapshotTask, snapshotMetadataTask);

        var snapshotContainer = new SnapshotContainer(
            snapshotTask.Result,
            snapshotMetadataTask.Result);

        return snapshotContainer;
    }

    private Task LoadSnapshotContainerAsync(SnapshotContainer snapshotContainer, CancellationToken cancellationToken)
    {
        if (SnapshotVersion.HasValue)
        {
            throw new InvalidOperationException($"Aggregate '{Id}' of type '{GetType().PrettyPrint()}' already has snapshot loaded");
        }

        if (Version > 0)
        {
            throw new InvalidOperationException($"Aggregate '{Id}' of type '{GetType().PrettyPrint()}' already has events loaded");
        }

        if (!(snapshotContainer.Snapshot is TSnapshot snapshot))
        {
            throw new ArgumentException($"Snapshot '{snapshotContainer.Snapshot.GetType().PrettyPrint()}' for aggregate '{GetType().PrettyPrint()}' is not of type '{typeof(TSnapshot).PrettyPrint()}'. Did you forget to implement a snapshot upgrader?");
        }

        SnapshotVersion = snapshotContainer.Metadata.AggregateSequenceNumber;

        AddPreviousSourceIds(snapshotContainer.Metadata.PreviousSourceIds);

        return LoadSnapshotAsync(
            snapshot,
            snapshotContainer.Metadata,
            cancellationToken);
    }

    protected abstract Task<TSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken);

    protected abstract Task LoadSnapshotAsync(TSnapshot snapshot, ISnapshotMetadata metadata, CancellationToken cancellationToken);

    protected virtual Task<ISnapshotMetadata> CreateSnapshotMetadataAsync(
        ISourceId sourceId,
        CancellationToken cancellationToken)
    {
        // We need to append the current source ID that triggered the snapshot
        // as this hasn't been loaded via the event stream
        var sourceIds = PreviousSourceIds
            .Append(sourceId)
            .ToArray();

        var snapshotMetadata = (ISnapshotMetadata)new SnapshotMetadata
        {
            AggregateId = Id.Value,
            AggregateName = Name.Value,
            AggregateSequenceNumber = Version,
            PreviousSourceIds = sourceIds
        };

        return Task.FromResult(snapshotMetadata);
    }
}