using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using EventFlow.ReadStores;

namespace MyTelegram.EventFlow;

//public interface IQueueKeyHelper
//{

//}

//public interface IKeyedQueue
//{

//}

public interface IQueuedReadModelDomainEventPublisher
{
    void Enqueue(IReadOnlyCollection<IDomainEvent> domainEvents);
    Task ProcessAsync(CancellationToken cancellationToken = default);
}

public class QueuedReadModelDomainEventPublisher : IQueuedReadModelDomainEventPublisher, IDispatchToReadStores
{
    private readonly Channel<IDomainEvent>[] _channels;
    private const int QueueCount = 128;
    private const int Mask = QueueCount - 1;

    public QueuedReadModelDomainEventPublisher()
    {
        _channels = new Channel<IDomainEvent>[QueueCount];
        for (int i = 0; i < QueueCount; i++)
        {
            _channels[i] = Channel.CreateUnbounded<IDomainEvent>(new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = false
            });
        }
    }

    public Task DispatchAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Enqueue(IReadOnlyCollection<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            var queue = _channels[GetShard(domainEvent.GetIdentity().Value)];
            queue.Writer.TryWrite(domainEvent);
        }
    }

    public Task ProcessAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    private async Task ProcessCoreAsync(IDomainEvent domainEvent)
    {

    }

    private int GetShard(long id)
    {
        unchecked
        {
            ulong x = (ulong)id;

            // MurmurHash3 finalizer
            x ^= x >> 33;
            x *= 0xff51afd7ed558ccdUL;
            x ^= x >> 33;

            return (int)x & Mask;
        }
    }

    private int GetShard(string aggregateId)
    {
        unchecked
        {
            ulong hash = 14695981039346656037UL;

            var span = aggregateId.AsSpan();

            foreach (var c in span)
            {
                hash ^= c;
                hash *= 1099511628211UL;
            }

            hash ^= hash >> 33;
            hash *= 0xff51afd7ed558ccd;
            hash ^= hash >> 33;

            return (int)hash & Mask;
        }
    }
}

//public record WrappedData<TData>(TData data)
