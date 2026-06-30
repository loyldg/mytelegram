namespace MyTelegram.EventFlow;

public class MyEventStoreBase(
    ILogger<MyEventStoreBase> logger,
    IAggregateFactory aggregateFactory,
    IEventJsonSerializer eventJsonSerializer,
    IEventUpgradeManager eventUpgradeManager,
    IEnumerable<IMetadataProvider> metadataProviders,
    IEventPersistence eventPersistence,
    ISnapshotStore snapshotStore,
    IInMemoryEventPersistence inMemoryEventPersistence,
    INullEventPersistence nullEventPersistence)
    : IEventStore
{
    private readonly IReadOnlyCollection<IMetadataProvider> _metadataProviders = metadataProviders.ToList();

    public virtual async Task<IReadOnlyCollection<IDomainEvent<TAggregate, TIdentity>>> StoreAsync<TAggregate, TIdentity>(
        TIdentity id,
        IReadOnlyCollection<IUncommittedEvent>? uncommittedDomainEvents,
        ISourceId sourceId,
        CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity
    {
        if (id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        if (sourceId.IsNone())
        {
            throw new ArgumentNullException(nameof(sourceId));
        }

        if (uncommittedDomainEvents == null || uncommittedDomainEvents.Count == 0)
        {
            return Array.Empty<IDomainEvent<TAggregate, TIdentity>>();
        }

        var aggregateType = typeof(TAggregate);
        logger.LogTrace(
            "Storing {UncommittedDomainEventsCount} events for aggregate {AggregateType} with ID {Id}",
            uncommittedDomainEvents.Count,
            aggregateType.PrettyPrint(),
            id);

        var batchId = Guid.NewGuid().ToString();
        var storeMetadata = new[]
        {
            new KeyValuePair<string, string>(MetadataKeys.BatchId, batchId),
            new KeyValuePair<string, string>(MetadataKeys.SourceId, sourceId.Value)
        };

        var serializedEvents = uncommittedDomainEvents
            .Select(e =>
            {
                var md = _metadataProviders
                    .SelectMany(p => p.ProvideMetadata<TAggregate, TIdentity>(id, e.AggregateEvent, e.Metadata))
                    .Concat(e.Metadata)
                    .Concat(storeMetadata);
                return eventJsonSerializer.Serialize(e.AggregateEvent, md);
            })
            .ToList();

        var committedDomainEvents = await GetEventPersistence<TAggregate>().CommitEventsAsync(
                id,
                serializedEvents,
                cancellationToken)
     ;

        var domainEvents = committedDomainEvents
            .Select(e => eventJsonSerializer.Deserialize<TAggregate, TIdentity>(id, e))
            .ToList();

        return domainEvents;
    }

    public async Task<AllEventsPage> LoadAllEventsAsync(
        GlobalPosition globalPosition,
        int pageSize,
        IEventUpgradeContext eventUpgradeContext,
        CancellationToken cancellationToken)
    {
        if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

        var allCommittedEventsPage = await eventPersistence.LoadAllCommittedEvents(
                globalPosition,
                pageSize,
                cancellationToken)
            .ConfigureAwait(false);
        var domainEvents = (IReadOnlyCollection<IDomainEvent>)allCommittedEventsPage.CommittedDomainEvents
            .Select(e => eventJsonSerializer.Deserialize(e))
            .ToList();
        //IAsyncEnumerable<IDomainEvent> a=new 
        
        // TODO: Pass a real IAsyncEnumerable instead
        domainEvents = await eventUpgradeManager.UpgradeAsync(
            domainEvents.ToAsyncEnumerable(),
            eventUpgradeContext,
            cancellationToken).ToArrayAsync(cancellationToken);

        return new AllEventsPage(allCommittedEventsPage.NextGlobalPosition, domainEvents);
    }

    public Task<IReadOnlyCollection<IDomainEvent<TAggregate, TIdentity>>> LoadEventsAsync<TAggregate, TIdentity>(
        TIdentity id,
        CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity
    {
        return LoadEventsAsync<TAggregate, TIdentity>(
            id,
            1,
            cancellationToken);
    }

    public Task<IReadOnlyCollection<IDomainEvent<TAggregate, TIdentity>>> LoadEventsAsync<TAggregate, TIdentity>(TIdentity id, int fromSequenceNumber, int toSequenceNumber,
        CancellationToken cancellationToken) where TAggregate : IAggregateRoot<TIdentity> where TIdentity : IIdentity
    {
        throw new NotImplementedException();
    }

    public virtual async Task<IReadOnlyCollection<IDomainEvent<TAggregate, TIdentity>>> LoadEventsAsync<TAggregate, TIdentity>(
        TIdentity id,
        int fromEventSequenceNumber,
        CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity
    {
        if (fromEventSequenceNumber < 1) throw new ArgumentOutOfRangeException(nameof(fromEventSequenceNumber), "Event sequence numbers start at 1");

        var committedDomainEvents = await GetEventPersistence<TAggregate>().LoadCommittedEventsAsync(
                id,
                fromEventSequenceNumber,
                cancellationToken)
            .ConfigureAwait(false);
        var domainEvents = (IReadOnlyCollection<IDomainEvent<TAggregate, TIdentity>>)committedDomainEvents
            .Select(e => eventJsonSerializer.Deserialize<TAggregate, TIdentity>(id, e))
            .ToList();

        if (domainEvents.Count == 0)
        {
            return domainEvents;
        }

        // TODO: Pass a real IAsyncEnumerable instead
        domainEvents = await eventUpgradeManager.UpgradeAsync(
            domainEvents.ToAsyncEnumerable(),
            cancellationToken).ToArrayAsync(cancellationToken);

        return domainEvents;
    }

    public virtual async Task<TAggregate> LoadAggregateAsync<TAggregate, TIdentity>(
        TIdentity id,
        CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity
    {
        var aggregate = await aggregateFactory.CreateNewAggregateAsync<TAggregate, TIdentity>(id);
        await aggregate.LoadAsync(this, snapshotStore, cancellationToken);
        return aggregate;
    }

    public Task DeleteAggregateAsync<TAggregate, TIdentity>(
        TIdentity id,
        CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity
    {
        return GetEventPersistence<TAggregate>().DeleteEventsAsync(
            id,
            cancellationToken);
    }

    private IEventPersistence GetEventPersistence<TAggregate>()
    {
        if (typeof(ISkipAggregateEvents).IsAssignableFrom(typeof(TAggregate)))
        {
            return nullEventPersistence;
        }

        if (typeof(IInMemoryAggregate).IsAssignableFrom(typeof(TAggregate)))
        {
            return inMemoryEventPersistence;
        }

        return eventPersistence;
    }
}