namespace MyTelegram.Domain.EventFlow;

public interface IMyInMemorySnapshotPersistence //:ISnapshotPersistence
{
    Task DeleteSnapshotAsync<TAggregate>(IIdentity identity,
        CancellationToken cancellationToken);

    Task<SnapshotContainer?> GetSnapshotAsync<TAggregate>(
        IIdentity identity,
        CancellationToken cancellationToken);

    Task SetSnapshotAsync<TAggregate>(
        IIdentity identity,
        SnapshotContainer snapshotContainer,
        CancellationToken cancellationToken);
}
