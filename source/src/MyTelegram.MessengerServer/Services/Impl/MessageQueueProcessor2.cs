namespace MyTelegram.MessengerServer.Services.Impl;

public class MessageQueueProcessor2<TData> : IMessageQueueProcessor<TData>
{
    private const int MaxQueueCount = 1000;
    private readonly IDataProcessor<TData> _dataProcessor;
    private readonly ILogger<MessageQueueProcessor2<TData>> _logger;
    private readonly ConcurrentDictionary<long, Channel<TData>> _queues = new();
    private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);
    private bool _isInited;

    public MessageQueueProcessor2(ILogger<MessageQueueProcessor2<TData>> logger,
        IDataProcessor<TData> dataProcessor)
    {
        _logger = logger;
        _dataProcessor = dataProcessor;
    }

    public async Task ProcessAsync()
    {
        InitQueueIfNeed();
        var tasks = new List<Task>();
        foreach (var queue in _queues)
        {
            var task = Task.Run(async () =>
            {
                while (await queue.Value.Reader.WaitToReadAsync().ConfigureAwait(false))
                {
                    while (queue.Value.Reader.TryRead(out var item))
                    {
                        try
                        {
                            //await func(item);
                            await _dataProcessor.ProcessAsync(item);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Process message queue error:");
                        }
                    }
                }
            });

            tasks.Add(task);
        }

        await Task.WhenAll(tasks);
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
        if (!_isInited)
        {
            _semaphoreSlim.Wait();
            for (var i = 0; i < MaxQueueCount; i++)
            {
                _queues.TryAdd(i, Channel.CreateUnbounded<TData>());
            }

            _isInited = true;
            _semaphoreSlim.Release();
        }
    }

    private bool TryGetQueue(long key,
        out Channel<TData>? queue)
    {
        var n = Math.Abs(key % MaxQueueCount);
        if (_queues.TryGetValue(n, out queue))
        {
            return true;
        }

        _logger.LogWarning("Can not find queue for key {Key}", key);
        return false;
    }
}
