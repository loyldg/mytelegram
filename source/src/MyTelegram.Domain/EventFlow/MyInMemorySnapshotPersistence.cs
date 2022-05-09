using System.Collections.Concurrent;

namespace MyTelegram.Domain.EventFlow;

public class MyInMemorySnapshotPersistence : IMyInMemorySnapshotPersistence
{
    private readonly ILogger<MyInMemorySnapshotPersistence> _logger;
    private readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, SnapshotContainer>> _snapshots = new();
    private long _totalCount;

    public MyInMemorySnapshotPersistence(ILogger<MyInMemorySnapshotPersistence> logger)
    {
        _logger = logger;
    }

    public Task<SnapshotContainer?> GetSnapshotAsync<TAggregate>(IIdentity identity,
        CancellationToken cancellationToken)
    {
        var type = typeof(TAggregate);
        if (_snapshots.TryGetValue(type, out var snapshotContainers))
        {
            if (snapshotContainers.TryGetValue(identity.Value, out var snapshotContainer))
            {
                return Task.FromResult<SnapshotContainer?>(snapshotContainer);
            }
        }

        return Task.FromResult<SnapshotContainer?>(null);
    }

    public Task SetSnapshotAsync<TAggregate>(IIdentity identity,
        SnapshotContainer snapshotContainer,
        CancellationToken cancellationToken)
    {
        var type = typeof(TAggregate);
        if (_snapshots.TryGetValue(type, out var snapshotContainers))
        {
            if (!snapshotContainers.TryRemove(identity.Value, out _))
            {
                Interlocked.Increment(ref _totalCount);
            }

            snapshotContainers.TryAdd(identity.Value, snapshotContainer);
        }
        else
        {
            var containers = new ConcurrentDictionary<string, SnapshotContainer>();
            containers.TryAdd(identity.Value, snapshotContainer);
            _snapshots.TryAdd(type, containers);
            Interlocked.Increment(ref _totalCount);
        }

        if (_totalCount % 1000 == 0)
        {
            _logger.LogInformation("Aggregate cache count:{Count}", _totalCount);
        }

        return Task.CompletedTask;
    }

    public Task DeleteSnapshotAsync<TAggregate>(IIdentity identity,
        CancellationToken cancellationToken)
    {
        var type = typeof(TAggregate);
        if (_snapshots.TryGetValue(type, out var snapshotContainers))
        {
            snapshotContainers.TryRemove(identity.Value, out _);
        }

        return Task.CompletedTask;
    }
}