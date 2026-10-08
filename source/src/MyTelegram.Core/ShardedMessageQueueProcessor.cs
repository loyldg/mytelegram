using System.Threading.Channels;
using Microsoft.Extensions.Logging;

namespace MyTelegram.Core;

public class ShardedMessageQueueProcessor<TData> : IShardedMessageQueueProcessor<TData>
{
    private readonly IDataProcessor<TData> _dataProcessor;
    private readonly ILogger<ShardedMessageQueueProcessor<TData>> _logger;
    private readonly Channel<TData>[] _channels;
    private const int QueueCount = 128;
    private const int Mask = QueueCount - 1;

    public ShardedMessageQueueProcessor(IDataProcessor<TData> dataProcessor, ILogger<ShardedMessageQueueProcessor<TData>> logger)
    {
        _dataProcessor = dataProcessor;
        _logger = logger;
        _channels = new Channel<TData>[QueueCount];
        for (int i = 0; i < QueueCount; i++)
        {
            _channels[i] = Channel.CreateUnbounded<TData>(new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = false
            });
        }
    }

    public void Enqueue(TData data, long key)
    {
        var queue = _channels[GetShard(key)];
        queue.Writer.TryWrite(data);
    }
    public void Enqueue(TData data, string key)
    {
        var queue = _channels[GetShard(key)];
        queue.Writer.TryWrite(data);
    }
    public async Task ProcessAsync(
        CancellationToken cancellationToken = default)
    {
        var tasks = new Task[QueueCount];

        for (var i = 0; i < tasks.Length; i++)
        {
            tasks[i] = ProcessCoreAsync(
                _channels[i],
                cancellationToken);
        }

        await Task.WhenAll(tasks);
    }

    private async Task ProcessCoreAsync(
        Channel<TData> channel,
        CancellationToken cancellationToken)
    {
        try
        {
            await foreach (var item in channel.Reader.ReadAllAsync(
                               cancellationToken))
            {
                try
                {
                    await _dataProcessor.ProcessAsync(
                        item,
                        cancellationToken);
                }
                catch (OperationCanceledException)
                    when (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Process sharded queue failed.");
                }
            }
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {
            // Normal shutdown.
        }
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