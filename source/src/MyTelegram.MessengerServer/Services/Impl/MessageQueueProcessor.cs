namespace MyTelegram.MessengerServer.Services.Impl;

public class MessageQueueProcessor<TData> : IMessageQueueProcessor<TData>
{
    //private readonly Channel<TData> _queue = Channel.CreateUnbounded<TData>();
    private const int MaxQueueCount = 1000;
    private readonly IDataProcessor<TData> _dataProcessor;
    private readonly ILogger<MessageQueueProcessor<TData>> _logger;
    private readonly ConcurrentDictionary<long, BlockingCollection<TData>> _queues = new();
    private bool _isInited;

    public MessageQueueProcessor(ILogger<MessageQueueProcessor<TData>> logger,
        IDataProcessor<TData> dataProcessor)
    {
        _logger = logger;
        _dataProcessor = dataProcessor;
    }

    public async Task ProcessAsync()
    {
        InitQueueIfNeed();
        //while (await _queue.Reader.WaitToReadAsync())
        //{
        //    while (_queue.Reader.TryRead(out var item))
        //    {
        //        await func(item).ConfigureAwait(false);
        //    }
        //}

        //await Parallel.ForEachAsync(_queues,
        //     async (queue,
        //         _) => {
        //             foreach (var item in queue.Value.GetConsumingEnumerable())
        //             {
        //                 try
        //                 {
        //                     await _dataProcessor.ProcessAsync(item).ConfigureAwait(false);
        //                 }
        //                 catch (Exception ex)
        //                 {
        //                     _logger.LogError(ex, "Process message queue error:");
        //                 }
        //             }
        //         }).ConfigureAwait(false);

        var tasks = new List<Task>();
        foreach (var queue in _queues)
        {
            var task = Task.Run(async () =>
            {
                foreach (var item in queue.Value.GetConsumingEnumerable())
                {
                    try
                    {
                        await _dataProcessor.ProcessAsync(item).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Process message queue error:");
                    }
                }
            });

            tasks.Add(task);
        }

        await Task.WhenAll(tasks).ConfigureAwait(false);
        //return Task.CompletedTask;
    }

    public void Enqueue(TData message,
        long key)
    {
        var queue = GetQueue(key);
        queue?.TryAdd(message);
    }

    private BlockingCollection<TData>? GetQueue(long key)
    {
        var n = Math.Abs(key % MaxQueueCount);
        if (!_queues.TryGetValue(n, out var queue))
        {
            _logger.LogWarning("Can not find queue for key {Key}", key);
        }

        return queue;
    }

    private void InitQueueIfNeed()
    {
        if (!_isInited)
        {
            for (var i = 0; i < MaxQueueCount; i++)
            {
                _queues.TryAdd(i, new BlockingCollection<TData>());
            }

            _isInited = true;
        }
    }
}