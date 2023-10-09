namespace MyTelegram.Domain.EventFlow;

public interface IMyInMemorySnapshotPersistence//:ISnapshotPersistence
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

public interface INullEventPersistence : IEventPersistence
{

}

public class NullEventPersistence : INullEventPersistence
{

    public class MyInMemoryCommittedDomainEvent : ICommittedDomainEvent
    {
        public string AggregateId { get; init; }
        public string Data { get; init; }
        public string Metadata { get; init; }
        public int AggregateSequenceNumber { get; init; }
    }

    public Task<AllCommittedEventsPage> LoadAllCommittedEvents(GlobalPosition globalPosition, int pageSize, CancellationToken cancellationToken)
    {
        return Task.FromResult(new AllCommittedEventsPage(GlobalPosition.Start, Array.Empty<ICommittedDomainEvent>()));
    }

    public async Task<IReadOnlyCollection<ICommittedDomainEvent>> CommitEventsAsync(IIdentity id, IReadOnlyCollection<SerializedEvent> serializedEvents, CancellationToken cancellationToken)
    {
        if (!serializedEvents.Any())
        {
            return new ICommittedDomainEvent[] { };
        }

        var eventDataModels = serializedEvents
            .Select((e, i) => new MyInMemoryCommittedDomainEvent
            {
                AggregateId = id.Value,
                AggregateSequenceNumber = e.AggregateSequenceNumber,
                Data = e.SerializedData,
                Metadata = e.SerializedMetadata,
            })
            .OrderBy(x => x.AggregateSequenceNumber)
            .ToList();

        return eventDataModels;
    }

    public Task<IReadOnlyCollection<ICommittedDomainEvent>> LoadCommittedEventsAsync(IIdentity id, int fromEventSequenceNumber, CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyCollection<ICommittedDomainEvent>>(Array.Empty<ICommittedDomainEvent>());
    }

    public Task DeleteEventsAsync(IIdentity id, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
