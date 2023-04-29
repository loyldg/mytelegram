namespace MyTelegram.Domain.EventFlow;

public class SnapshotWithInMemoryCacheStore : ISnapshotWithInMemoryCacheStore
{
    private readonly IMyInMemorySnapshotPersistence _inMemorySnapshotPersistence;
    private readonly ILogger<SnapshotStore> _logger;

    private readonly ISnapshotPersistence _snapshotPersistence;

    //private readonly ISnapshotSerializer _snapshotSerializer;
    private readonly ISnapshotSerializer _snapshotSerializer;

    public SnapshotWithInMemoryCacheStore(IMyInMemorySnapshotPersistence inMemorySnapshotPersistence,
        ISnapshotSerializer snapshotSerializer,
        ISnapshotPersistence snapshotPersistence,
        ILogger<SnapshotStore> logger)
    {
        _inMemorySnapshotPersistence = inMemorySnapshotPersistence;
        _snapshotSerializer = snapshotSerializer;
        _snapshotPersistence = snapshotPersistence;
        _logger = logger;
    }

    public async Task<SnapshotContainer?> LoadSnapshotAsync<TAggregate, TIdentity, TSnapshot>(TIdentity identity,
        CancellationToken cancellationToken) where TAggregate : ISnapshotAggregateRoot<TIdentity, TSnapshot>
        where TIdentity : IIdentity
        where TSnapshot : ISnapshot
    {
        var cachedSnapshot =
                await LoadSnapshotFromMemoryAsync<TAggregate, TIdentity, TSnapshot>(identity, cancellationToken)
            ;
        if (cachedSnapshot != null)
        {
            _logger.LogTrace("Fetching snapshot for {AggregateType} with ID {Id} from memory",
                typeof(TAggregate).PrettyPrint(),
                identity);
            return cachedSnapshot;
        }

        _logger.LogTrace(
            "Fetching snapshot for {AggregateType} with ID {Id}",
            typeof(TAggregate).PrettyPrint(),
            identity);
        var committedSnapshot = await _snapshotPersistence.GetSnapshotAsync(
                typeof(TAggregate),
                identity,
                cancellationToken)
            ;
        if (committedSnapshot == null)
        {
            _logger.LogTrace(
                "No snapshot found for {AggregateType} with ID {Id}",
                typeof(TAggregate).PrettyPrint(),
                identity);
            return null;
        }

        var snapshotContainer = await _snapshotSerializer.DeserializeAsync<TAggregate, TIdentity, TSnapshot>(
                committedSnapshot,
                cancellationToken)
            ;

        return snapshotContainer;
    }

    public async Task StoreSnapshotAsync<TAggregate, TIdentity, TSnapshot>(TIdentity identity,
        SnapshotContainer snapshotContainer,
        CancellationToken cancellationToken) where TAggregate : ISnapshotAggregateRoot<TIdentity, TSnapshot>
        where TIdentity : IIdentity
        where TSnapshot : ISnapshot
    {
        var serializedSnapshot = await _snapshotSerializer.SerializeAsync<TAggregate, TIdentity, TSnapshot>(
                snapshotContainer,
                cancellationToken)
            ;

        await _snapshotPersistence.SetSnapshotAsync(
                typeof(TAggregate),
                identity,
                serializedSnapshot,
                cancellationToken)
            ;
    }

    public Task<SnapshotContainer?> LoadSnapshotFromMemoryAsync<TAggregate, TIdentity, TSnapshot>(TIdentity identity,
        CancellationToken cancellationToken) where TAggregate : ISnapshotAggregateRoot<TIdentity, TSnapshot>
        where TIdentity : IIdentity
        where TSnapshot : ISnapshot
    {
        return _inMemorySnapshotPersistence.GetSnapshotAsync<TAggregate>(identity, cancellationToken);
        //var committedSnapshot = await _inMemorySnapshotPersistence.GetSnapshotAsync(
        //        typeof(TAggregate),
        //        identity,
        //        cancellationToken)
        //    ;
        //if (committedSnapshot == null)
        //{
        //    _logger.LogTrace(
        //        "No snapshot found for {AggregateType} with ID {Id} from memory",
        //        typeof(TAggregate).PrettyPrint(),
        //        identity);
        //    return null;
        //}

        //var snapshotContainer = await _snapshotSerilizer.DeserializeAsync<TAggregate, TIdentity, TSnapshot>(
        //        committedSnapshot,
        //        cancellationToken)
        //    ;

        //return snapshotContainer;
    }

    public Task StoreInMemorySnapshotAsync<TAggregate, TIdentity, TSnapshot>(TIdentity identity,
        SnapshotContainer snapshotContainer,
        CancellationToken cancellationToken) where TAggregate : ISnapshotAggregateRoot<TIdentity, TSnapshot>
        where TIdentity : IIdentity
        where TSnapshot : ISnapshot
    {
        return _inMemorySnapshotPersistence.SetSnapshotAsync<TAggregate>(identity,
            snapshotContainer,
            cancellationToken);
        //var serializedSnapshot = await _snapshotSerilizer.SerializeAsync<TAggregate, TIdentity, TSnapshot>(
        //        snapshotContainer,
        //        cancellationToken)
        //    ;

        //await _inMemorySnapshotPersistence.SetSnapshotAsync(
        //        typeof(TAggregate),
        //        identity,
        //        serializedSnapshot,
        //        cancellationToken)
        //    ;
    }
}
