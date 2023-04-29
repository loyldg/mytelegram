namespace MyTelegram.Domain.EventFlow;

public interface ISnapshotWithInMemoryCacheStore : ISnapshotStore
{
    Task<SnapshotContainer?> LoadSnapshotFromMemoryAsync<TAggregate, TIdentity, TSnapshot>(TIdentity identity,
        CancellationToken cancellationToken) where TAggregate : ISnapshotAggregateRoot<TIdentity, TSnapshot>
        where TIdentity : IIdentity
        where TSnapshot : ISnapshot;

    Task StoreInMemorySnapshotAsync<TAggregate, TIdentity, TSnapshot>(TIdentity identity,
        SnapshotContainer snapshotContainer,
        CancellationToken cancellationToken) where TAggregate : ISnapshotAggregateRoot<TIdentity, TSnapshot>
        where TIdentity : IIdentity
        where TSnapshot : ISnapshot;
}
