using System.Collections.Concurrent;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;

namespace MyTelegram.Services.Services;

public class MessageQueueProcessor2<TData> : IMessageQueueProcessor<TData>
{
    private const int MaxQueueCount = 1000;
    private readonly IDataProcessor<TData> _dataProcessor;
    private readonly ILogger<MessageQueueProcessor2<TData>> _logger;
    private readonly ConcurrentDictionary<long, Channel<TData>> _queues = new();
    private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);
    private bool _isInited;

    private readonly BlockingCollection<Channel<TData>> _channels = new();

    public MessageQueueProcessor2(ILogger<MessageQueueProcessor2<TData>> logger,
        IDataProcessor<TData> dataProcessor)
    {
        _logger = logger;
        _dataProcessor = dataProcessor;
    }

    public Task ProcessAsync()
    {
        InitQueueIfNeed();
        
        Task.Factory.StartNew(() =>
        {
            foreach (var queue in _channels.GetConsumingEnumerable())
            {
                Task.Run(async () =>
                {
                    while (await queue.Reader.WaitToReadAsync().ConfigureAwait(false))
                    {
                        while (queue.Reader.TryRead(out var item))
                        {
                            try
                            {
                                await _dataProcessor.ProcessAsync(item);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "Process message queue error:");
                            }
                        }
                    }
                });
            }
        }, TaskCreationOptions.LongRunning);

        //Task.Run(() =>
        //{
        //    foreach (var queue in _channels.GetConsumingEnumerable())
        //    {
        //        // Console.WriteLine("New queue");
        //        Task.Run(async () =>
        //        {
        //            while (await queue.Reader.WaitToReadAsync().ConfigureAwait(false))
        //            {
        //                while (queue.Reader.TryRead(out var item))
        //                {
        //                    try
        //                    {
        //                        //await func(item);
        //                        await _dataProcessor.ProcessAsync(item);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        _logger.LogError(ex, "Process message queue error:");
        //                    }
        //                }
        //            }
        //        });
        //    }
        //});

        return Task.CompletedTask;
    }

    public void Enqueue(TData message,
        long key)
    {
        if (!TryGetQueue(key, out var queue))
        {
            InitQueueIfNeed();
            if (!TryGetQueue(key, out queue))
            {
                throw new InvalidOperationException($"Get queue failed,key={key} message='{typeof(TData)}'");
            }
        }

        if (!queue!.Writer.TryWrite(message))
        {
            _logger.LogWarning("Can not write message to queue");
        }
    }

    private void InitQueueIfNeed()
    {
    }

    private bool TryGetQueue(long key,
        out Channel<TData>? queue)
    {
        var queueKey = key % MaxQueueCount;
        if (_queues.TryGetValue(queueKey, out queue))
        {
            return true;
        }

        queue = Channel.CreateUnbounded<TData>();
        _queues.TryAdd(queueKey, queue);

        _channels.Add(queue);


        return true;
    }
}