using EventFlow.Exceptions;
using System.Collections.Concurrent;

namespace MyTelegram.EventFlow;

public sealed class MyInMemoryEventPersistence : IInMemoryEventPersistence
{
    private readonly ConcurrentDictionary<string, EventStream> _eventStore = new();
    private readonly ILogger<MyInMemoryEventPersistence> _logger;
    private readonly IDomainEventFactory _domainEventFactory;

    public MyInMemoryEventPersistence(ILogger<MyInMemoryEventPersistence> logger,
        IDomainEventFactory domainEventFactory)
    {
        _logger = logger;
        _domainEventFactory = domainEventFactory;

#if DEBUG
        _ = Task.Run(async () =>
        {
            while (true)
            {
                //var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                if (_eventStore.Count > 0)
                {
                    Console.WriteLine($"In-memory aggregates count: {_eventStore.Count}");
                }

                if (_eventStore.Count > 50)
                {
                    var first = _eventStore.FirstOrDefault();
                    Console.WriteLine(
                        $"First:{first.Key} expiresAt={first.Value.ExpiresAt} version={first.Value.Version} events.count={first.Value.Events.Count}");
                }

                await Task.Delay(3000);
            }
        });
#endif
    }

    public Task<AllCommittedEventsPage> LoadAllCommittedEvents(
        GlobalPosition globalPosition,
        int pageSize,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //var startPosition = globalPosition.IsStart
        //    ? 0
        //    : long.Parse(globalPosition.Value);

        //var result = new List<ICommittedDomainEvent>(pageSize);

        //long nextPosition = startPosition;

        //foreach (var stream in _eventStore.Values)
        //{
        //    var events = stream.Events;

        //    var count = events.Count;

        //    for (var i = 0; i < count; i++)
        //    {
        //        var e = events[i];

        //        if (e.GlobalSequenceNumber < startPosition)
        //            continue;

        //        result.Add(e);

        //        if (result.Count >= pageSize)
        //            break;
        //    }

        //    if (result.Count >= pageSize)
        //        break;
        //}

        //if (result.Count > 0)
        //{
        //    var last = result[^1];

        //    nextPosition = last.GlobalSequenceNumber + 1;
        //}

        //return Task.FromResult(
        //    new AllCommittedEventsPage(
        //        new GlobalPosition(nextPosition.ToString()),
        //        result));
    }

    public Task<IReadOnlyCollection<ICommittedDomainEvent>> CommitEventsAsync(
        IIdentity id,
        IReadOnlyCollection<SerializedEvent> serializedEvents,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<ICommittedDomainEvent>> LoadCommittedEventsAsync(
        IIdentity id,
        int fromEventSequenceNumber,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //if (!_eventStore.TryGetValue(id.Value, out var stream))
        //{
        //    return Task.FromResult<IReadOnlyCollection<ICommittedDomainEvent>>(
        //        Array.Empty<ICommittedDomainEvent>());
        //}

        //var events = stream.Events;

        //if (fromEventSequenceNumber <= 1)
        //{
        //    return Task.FromResult<
        //        IReadOnlyCollection<ICommittedDomainEvent>>(events);
        //}

        //var result = new List<ICommittedDomainEvent>(events.Count);
        //var count = events.Count;

        //for (var i = 0; i < count; i++)
        //{
        //    var e = events[i];

        //    if (e.AggregateSequenceNumber >= fromEventSequenceNumber)
        //    {
        //        result.Add(e);
        //    }
        //}

        //return Task.FromResult<
        //    IReadOnlyCollection<ICommittedDomainEvent>>(result);
    }

    public Task<IReadOnlyCollection<ICommittedDomainEvent>> LoadCommittedEventsAsync(IIdentity id, int fromEventSequenceNumber, int toEventSequenceNumber,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteEventsAsync(
        IIdentity id,
        CancellationToken cancellationToken)
    {
        if (_eventStore.TryRemove(
                id.Value,
                out var stream))
        {
            _logger.LogTrace(
                "Deleted entity {Id} with {Count} events",
                id.Value,
                stream.Events.Count);

#if DEBUG
            Console.WriteLine($"[Removed] In-memory aggregates count: {_eventStore.Count}");
#endif
        }

        return Task.CompletedTask;
    }

    public IReadOnlyCollection<IDomainEvent<TAggregate, TIdentity>> CommitEvents<TAggregate, TIdentity>(TIdentity id,
        IReadOnlyCollection<IUncommittedEvent> events) where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity
    {
        var stream =
            _eventStore.GetOrAdd(
                id.Value,
                static _ => new EventStream());

        var committed =
            new IDomainEvent<TAggregate, TIdentity>[events.Count];

        var index = 0;

        var lockTaken = false;

        try
        {
            stream.Lock.Enter(ref lockTaken);
            if (stream.Version !=
                events.First().Metadata.AggregateSequenceNumber - 1)
            {
                throw new OptimisticConcurrencyException(string.Empty);
            }

            foreach (var e in events)
            {
                //var eventDefinition = _eventDefinitionService.GetDefinition(e.AggregateEvent.GetType());
                //var metadata = new Metadata(e.Metadata
                //    .Where(kv => kv.Key != MetadataKeys.EventName && kv.Key != MetadataKeys.EventVersion)
                //    .Concat([
                //        new KeyValuePair<string, string>(MetadataKeys.EventName, eventDefinition.Name),
                //        new KeyValuePair<string, string>(MetadataKeys.EventVersion, eventDefinition.Version.ToString(CultureInfo.InvariantCulture)),
                //    ]));
                //sw.Stop();
                var metadata = e.Metadata.CloneWith(new KeyValuePair<string, string>(MetadataKeys.EventName,e.AggregateEvent.GetType().Name), new KeyValuePair<string, string>(MetadataKeys.EventVersion,"1"));

                var domainEvent = _domainEventFactory.Create<TAggregate, TIdentity>(e.AggregateEvent, metadata, id, e.Metadata.AggregateSequenceNumber);
                committed[index++] = domainEvent;

                stream.Version =
                    e.Metadata.AggregateSequenceNumber;
            }

            var old =
                stream.Events;
            old.AddRange(committed);
        }
        finally
        {
            if (lockTaken)
            {
                stream.Lock.Exit();
            }
        }

        return committed;
    }

    public Task<IReadOnlyCollection<IDomainEvent<TAggregate, TIdentity>>> LoadEventsAsync<TAggregate, TIdentity>(
        TIdentity id, int fromEventSequenceNumber,
        CancellationToken cancellationToken) where TAggregate : IAggregateRoot<TIdentity> where TIdentity : IIdentity
    {
        if (!_eventStore.TryGetValue(
                id.Value,
                out var stream))
        {
            return Task.FromResult<
                IReadOnlyCollection<IDomainEvent<TAggregate, TIdentity>>>(
                Array.Empty<IDomainEvent<TAggregate, TIdentity>>());
        }

        var events = stream.Events;

        if (fromEventSequenceNumber <= 1)
        {
            return Task.FromResult<
                IReadOnlyCollection<IDomainEvent<TAggregate, TIdentity>>>(
                events.Cast<IDomainEvent<TAggregate, TIdentity>>().ToArray());
        }

        var result =
            new List<IDomainEvent<TAggregate, TIdentity>>(events.Count);

        foreach (var e in events)
        {
            if (e.AggregateSequenceNumber >= fromEventSequenceNumber)
            {
                result.Add(
                    (IDomainEvent<TAggregate, TIdentity>)e);
            }
        }

        return Task.FromResult<
            IReadOnlyCollection<IDomainEvent<TAggregate, TIdentity>>>(
            result);
    }

    public Task<IReadOnlyCollection<IDomainEvent<TAggregate, TIdentity>>> LoadEventsAsync<TAggregate, TIdentity>(TIdentity id, int fromEventSequenceNumber, int toEventSequenceNumber,
        CancellationToken cancellationToken) where TAggregate : IAggregateRoot<TIdentity> where TIdentity : IIdentity
    {
        throw new NotImplementedException();
    }

    private sealed class EventStream
    {
        //public List<InMemoryCommittedDomainEvent> Events = new(8);
        public List<IDomainEvent> Events = new();
        public readonly int ExpiresAt = (int)DateTimeOffset.UtcNow.AddSeconds(60).ToUnixTimeSeconds();
        public SpinLock Lock = new(false);
        public int Version;
    }

    private sealed class InMemoryCommittedDomainEvent : ICommittedDomainEvent
    {
        public long GlobalSequenceNumber { get; init; }
        public string AggregateName { get; init; } = string.Empty;
        public string AggregateId { get; init; } = string.Empty;
        public string Data { get; init; } = string.Empty;
        public string Metadata { get; init; } = string.Empty;
        public int AggregateSequenceNumber { get; init; }

        public override string ToString()
        {
            return $"{AggregateName} v{AggregateSequenceNumber}";
        }
    }
}