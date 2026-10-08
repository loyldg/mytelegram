using CommunityToolkit.HighPerformance.Buffers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Polly.Retry;
using StackExchange.Redis;

namespace MyTelegram.EventBus.Redis;

public sealed class RedisStreamEventBus(
    ILogger<RedisStreamEventBus> logger,
    IServiceProvider serviceProvider,
    IOptions<EventBusRedisOptions> options,
    IOptions<EventBusOptions> eventBusOptions,
    IRabbitMqSerializer serializer,
    IOptions<EventBusSubscriptionInfo> subscriptionOptions)
    : IEventBus, IHostedService, IDisposable
{
    private readonly EventBusRedisOptions _options = options.Value;
    private readonly EventBusOptions _eventBusOptions = eventBusOptions.Value;
    private readonly EventBusSubscriptionInfo _subscriptionInfo =
        subscriptionOptions.Value;

    private readonly ResiliencePipeline _publishPipeline =
        CreateResiliencePipeline(options.Value.RetryCount);

    private readonly CancellationTokenSource _disposeCts = new();

    private readonly string _consumerName =
        $"{Environment.MachineName}-{Guid.NewGuid():N}";

    private readonly Dictionary<string, string> _claimCursors =
        new(StringComparer.Ordinal);

    private ConnectionMultiplexer? _redis;
    private ConnectionMultiplexer? _consumerRedis;

    private IDatabase? _database;
    private IDatabase? _consumerDatabase;

    private Task? _consumerTask;
    private Task? _claimTask;
    private Task? _cleanupTask;

    private bool _started;

    private IReadOnlyDictionary<string, string> _streamToEventType =
        new Dictionary<string, string>();

    private string ConsumerGroupName =>
        _eventBusOptions.ClientName;

    public async Task PublishAsync<TEventData>(
        TEventData eventData,
        string? eventType = null)
        where TEventData : class
    {
        ArgumentNullException.ThrowIfNull(eventData);

        var actualEventType =
            eventType ?? eventData.GetType().Name;

        await _publishPipeline.ExecuteAsync(
            async cancellationToken =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                await PublishInternalAsync(
                    eventData,
                    actualEventType);

                return ValueTask.CompletedTask;
            },
            _disposeCts.Token);
    }

    private async Task PublishInternalAsync<TEventData>(
        TEventData eventData,
        string eventType)
        where TEventData : class
    {
        var database =
            _database ??
            throw new InvalidOperationException(
                "Redis connection is not initialized.");

        var streamName =
            GetStreamName(eventType);

        using var writer =
            new ArrayPoolBufferWriter<byte>();

        serializer.Serialize(
            writer,
            eventData);

        var arguments =
            new List<object>(10)
            {
                streamName,
                "ACKED"
            };

        if (_options.MaxStreamLength > 0)
        {
            arguments.Add("MAXLEN");
            arguments.Add("~");
            arguments.Add(_options.MaxStreamLength);
        }

        arguments.Add("*");
        arguments.Add("event_type");
        arguments.Add(eventType);
        arguments.Add("data");
        arguments.Add(writer.WrittenMemory);

        await database.ExecuteAsync(
            "XADD",
            arguments.ToArray());
    }

    public async Task StartAsync(
        CancellationToken cancellationToken)
    {
        if (_started)
        {
            return;
        }

        BuildStreamMap();

        if (_subscriptionInfo.EventTypes.Count == 0)
        {
            logger.LogWarning(
                "Redis Stream EventBus has no subscribed event types.");
        }

        await ConnectAsync(cancellationToken);

        try
        {
            await EnsureConsumerGroupsAsync(
                cancellationToken);

            _consumerTask =
                Task.Run(
                    () => ConsumeLoopAsync(
                        _disposeCts.Token),
                    CancellationToken.None);

            _claimTask =
                Task.Run(
                    () => ClaimLoopAsync(
                        _disposeCts.Token),
                    CancellationToken.None);

            if (_options.Retention > TimeSpan.Zero &&
                _options.CleanupInterval > TimeSpan.Zero)
            {
                _cleanupTask =
                    Task.Run(
                        () => CleanupLoopAsync(
                            _disposeCts.Token),
                        CancellationToken.None);
            }

            _started = true;

            logger.LogInformation(
                "Redis Stream EventBus started. Group={Group}, Consumer={Consumer}, Retention={Retention}, CleanupInterval={CleanupInterval}",
                ConsumerGroupName,
                _consumerName,
                _options.Retention,
                _options.CleanupInterval);
        }
        catch
        {
            _disposeCts.Cancel();

            await CloseConnectionsAsync();

            throw;
        }
    }

    private async Task ConnectAsync(
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var configuration =
            ConfigurationOptions.Parse(
                _options.ConnectionString);

        configuration.AbortOnConnectFail = false;
        configuration.ClientName =
            $"{ConsumerGroupName}-normal";

        _redis =
            await ConnectionMultiplexer.ConnectAsync(
                configuration);

        _database =
            _redis.GetDatabase();

        cancellationToken.ThrowIfCancellationRequested();

        var consumerConfiguration =
            ConfigurationOptions.Parse(
                _options.ConnectionString);

        consumerConfiguration.AbortOnConnectFail = false;

        var commandTimeout =
            Math.Max(
                5000,
                _options.BlockMilliseconds + 5000);

        consumerConfiguration.AsyncTimeout =
            commandTimeout;

        consumerConfiguration.SyncTimeout =
            commandTimeout;

        consumerConfiguration.ClientName =
            $"{ConsumerGroupName}-consumer";

        _consumerRedis =
            await ConnectionMultiplexer.ConnectAsync(
                consumerConfiguration);

        _consumerDatabase =
            _consumerRedis.GetDatabase();

        logger.LogInformation(
            "Redis Stream EventBus connected. Group={Group}, Consumer={Consumer}, BlockMilliseconds={BlockMilliseconds}, CommandTimeout={CommandTimeout}",
            ConsumerGroupName,
            _consumerName,
            _options.BlockMilliseconds,
            commandTimeout);
    }

    private async Task EnsureConsumerGroupsAsync(
        CancellationToken cancellationToken)
    {
        var database =
            _database ??
            throw new InvalidOperationException(
                "Redis connection is not initialized.");

        foreach (var eventType in
                 _subscriptionInfo.EventTypes.Keys)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var streamName =
                GetStreamName(eventType);

            await EnsureConsumerGroupAsync(
                database,
                streamName,
                cancellationToken);
        }
    }

    private async Task EnsureConsumerGroupAsync(
        IDatabase database,
        string streamName,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            await database.StreamCreateConsumerGroupAsync(
                key: streamName,
                groupName: ConsumerGroupName,
                position: "$",
                createStream: true);
        }
        catch (RedisServerException ex)
            when (ex.Message.StartsWith(
                "BUSYGROUP",
                StringComparison.OrdinalIgnoreCase))
        {
        }
    }

    private async Task ConsumeLoopAsync(
        CancellationToken cancellationToken)
    {
        var streams =
            _subscriptionInfo.EventTypes.Keys
                .Select(GetStreamName)
                .ToArray();

        if (streams.Length == 0)
        {
            return;
        }

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var database =
                    _consumerDatabase;

                if (database is null)
                {
                    await Task.Delay(
                        1000,
                        cancellationToken);

                    continue;
                }

                var arguments =
                    new List<object>(
                        8 + streams.Length * 2)
                    {
                        "GROUP",
                        ConsumerGroupName,
                        _consumerName,
                        "COUNT",
                        _options.BatchSize,
                        "BLOCK",
                        _options.BlockMilliseconds,
                        "STREAMS"
                    };

                foreach (var stream in streams)
                {
                    arguments.Add(stream);
                }

                foreach (var _ in streams)
                {
                    arguments.Add(">");
                }

                var result =
                    await database.ExecuteAsync(
                        "XREADGROUP",
                        arguments.ToArray());

                if (result.IsNull)
                {
                    continue;
                }

                await ProcessReadGroupResultAsync(
                    result,
                    cancellationToken);
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            catch (RedisConnectionException ex)
            {
                logger.LogWarning(
                    ex,
                    "Redis connection error in XREADGROUP loop.");

                await DelayReconnectAsync(
                    cancellationToken);
            }
            catch (RedisTimeoutException ex)
            {
                logger.LogWarning(
                    ex,
                    "Redis XREADGROUP timeout. BlockMilliseconds={BlockMilliseconds}, CommandTimeout={CommandTimeout}",
                    _options.BlockMilliseconds,
                    Math.Max(
                        5000,
                        _options.BlockMilliseconds + 5000));

                await DelayReconnectAsync(
                    cancellationToken);
            }
            catch (RedisServerException ex)
            {
                logger.LogWarning(
                    ex,
                    "Redis server error in XREADGROUP loop.");

                await DelayReconnectAsync(
                    cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Unexpected error in Redis Stream consumer loop.");

                await DelayReconnectAsync(
                    cancellationToken);
            }
        }
    }

    private async Task ProcessReadGroupResultAsync(
        RedisResult result,
        CancellationToken cancellationToken)
    {
        if (result.IsNull)
        {
            return;
        }

        if (!IsAggregateResult(result))
        {
            logger.LogWarning(
                "Unexpected XREADGROUP response type. Resp2Type={Resp2Type}, Resp3Type={Resp3Type}, Length={Length}, Value={Value}",
                result.Resp2Type,
                result.Resp3Type,
                result.Length,
                result);

            return;
        }

        var acknowledgements =
            new Dictionary<string, List<RedisValue>>(
                StringComparer.Ordinal);

        if (result.Resp3Type == ResultType.Map)
        {
            await ProcessResp3StreamMapAsync(
                result,
                acknowledgements,
                cancellationToken);
        }
        else
        {
            await ProcessResp2StreamArrayAsync(
                result,
                acknowledgements,
                cancellationToken);
        }

        foreach (var pair in acknowledgements)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await AcknowledgeAsync(
                pair.Key,
                pair.Value);
        }
    }

    private async Task ProcessResp2StreamArrayAsync(
        RedisResult result,
        Dictionary<string, List<RedisValue>> acknowledgements,
        CancellationToken cancellationToken)
    {
        for (var i = 0; i < result.Length; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var streamResult =
                result[i];

            if (streamResult.IsNull ||
                !IsAggregateResult(streamResult))
            {
                continue;
            }

            if (streamResult.Length < 2)
            {
                continue;
            }

            var streamName =
                (string?)streamResult[0];

            if (string.IsNullOrEmpty(streamName))
            {
                continue;
            }

            await ProcessStreamEntriesAsync(
                streamName,
                streamResult[1],
                acknowledgements,
                cancellationToken);
        }
    }

    private async Task ProcessResp3StreamMapAsync(
        RedisResult result,
        Dictionary<string, List<RedisValue>> acknowledgements,
        CancellationToken cancellationToken)
    {
        if (result.Length % 2 != 0)
        {
            logger.LogWarning(
                "Invalid RESP3 XREADGROUP map response. Length={Length}",
                result.Length);

            return;
        }

        for (var i = 0; i < result.Length; i += 2)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var streamName =
                (string?)result[i];

            if (string.IsNullOrEmpty(streamName))
            {
                continue;
            }

            var entries =
                result[i + 1];

            await ProcessStreamEntriesAsync(
                streamName,
                entries,
                acknowledgements,
                cancellationToken);
        }
    }

    private async Task ProcessStreamEntriesAsync(
        string streamName,
        RedisResult entriesResult,
        Dictionary<string, List<RedisValue>> acknowledgements,
        CancellationToken cancellationToken)
    {
        if (entriesResult.IsNull)
        {
            return;
        }

        if (!IsAggregateResult(entriesResult))
        {
            logger.LogWarning(
                "Invalid XREADGROUP entries response. Stream={Stream}, Resp2Type={Resp2Type}, Resp3Type={Resp3Type}, Length={Length}",
                streamName,
                entriesResult.Resp2Type,
                entriesResult.Resp3Type,
                entriesResult.Length);

            return;
        }

        if (!_streamToEventType.TryGetValue(
                streamName,
                out var eventType))
        {
            logger.LogWarning(
                "Received message from unknown stream {Stream}",
                streamName);

            return;
        }

        for (var i = 0;
             i < entriesResult.Length;
             i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entryResult =
                entriesResult[i];

            var entry =
                ParseStreamEntry(entryResult);

            if (entry.Id.IsNullOrEmpty)
            {
                continue;
            }

            var success = false;

            try
            {
                success =
                    await ProcessEventAsync(
                        eventType,
                        entry,
                        cancellationToken);
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogWarning(
                    ex,
                    "Error processing Redis Stream event. EventType={EventType}, MessageId={MessageId}",
                    eventType,
                    entry.Id);
            }

            if (!success)
            {
                continue;
            }

            if (!acknowledgements.TryGetValue(
                    streamName,
                    out var ids))
            {
                ids = [];
                acknowledgements.Add(
                    streamName,
                    ids);
            }

            ids.Add(entry.Id);
        }
    }

    private async Task<bool> ProcessEventAsync(
        string eventName,
        StreamEntry entry,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!_subscriptionInfo.EventTypes.TryGetValue(
                eventName,
                out var eventType))
        {
            logger.LogWarning(
                "No event type registered for {EventName}",
                eventName);

            return true;
        }

        var data =
            GetField(
                entry.Values,
                "data");

        if (data.IsNullOrEmpty)
        {
            logger.LogWarning(
                "Redis Stream message does not contain data. EventType={EventType}, MessageId={MessageId}",
                eventName,
                entry.Id);

            return true;
        }

        object eventData;

        try
        {
            eventData =
                serializer.Deserialize(
                    eventType,
                    data);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Failed to deserialize Redis Stream event. EventType={EventType}, MessageId={MessageId}",
                eventName,
                entry.Id);

            return true;
        }

        if (!_subscriptionInfo.TryGetHandlers(
                eventType,
                out var handlers))
        {
            return true;
        }

        foreach (var handler in handlers ?? [])
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                await handler(
                    eventData,
                    serviceProvider);
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogWarning(
                    ex,
                    "Event handler failed. EventType={EventType}, MessageId={MessageId}",
                    eventName,
                    entry.Id);

                return false;
            }
        }

        return true;
    }

    private async Task AcknowledgeAsync(
        string streamName,
        List<RedisValue> ids)
    {
        if (ids.Count == 0)
        {
            return;
        }

        var database =
            _database;

        if (database is null)
        {
            return;
        }

        var arguments =
            new object[ids.Count + 5];

        arguments[0] =
            streamName;

        arguments[1] =
            ConsumerGroupName;

        arguments[2] =
            "ACKED";

        arguments[3] =
            "IDS";

        arguments[4] =
            ids.Count;

        for (var i = 0;
             i < ids.Count;
             i++)
        {
            arguments[i + 5] =
                ids[i];
        }

        await database.ExecuteAsync(
            "XACKDEL",
            arguments);
    }

    private async Task ClaimLoopAsync(
        CancellationToken cancellationToken)
    {
        using var timer =
            new PeriodicTimer(
                TimeSpan.FromMilliseconds(
                    Math.Max(
                        1000,
                        _options.ClaimIntervalMilliseconds)));

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await timer.WaitForNextTickAsync(
                    cancellationToken);

                await ClaimPendingMessagesAsync(
                    cancellationToken);
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            catch (RedisConnectionException ex)
            {
                logger.LogWarning(
                    ex,
                    "Redis connection error in XAUTOCLAIM loop.");

                await DelayReconnectAsync(
                    cancellationToken);
            }
            catch (RedisTimeoutException ex)
            {
                logger.LogWarning(
                    ex,
                    "Redis timeout in XAUTOCLAIM loop.");

                await DelayReconnectAsync(
                    cancellationToken);
            }
            catch (RedisServerException ex)
            {
                logger.LogWarning(
                    ex,
                    "Redis server error in XAUTOCLAIM loop.");

                await DelayReconnectAsync(
                    cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Unexpected error in Redis Stream claim loop.");

                await DelayReconnectAsync(
                    cancellationToken);
            }
        }
    }

    private async Task ClaimPendingMessagesAsync(
        CancellationToken cancellationToken)
    {
        var database =
            _database;

        if (database is null)
        {
            return;
        }

        foreach (var eventName in
                 _subscriptionInfo.EventTypes.Keys)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var streamName =
                GetStreamName(eventName);

            var startId =
                _claimCursors.TryGetValue(
                    streamName,
                    out var cursor)
                    ? cursor
                    : "0-0";

            RedisResult result;

            try
            {
                result =
                    await database.ExecuteAsync(
                        "XAUTOCLAIM",
                        streamName,
                        ConsumerGroupName,
                        _consumerName,
                        _options.ClaimMinIdleMilliseconds,
                        startId,
                        "COUNT",
                        _options.BatchSize);
            }
            catch (RedisServerException ex)
                when (ex.Message.StartsWith(
                    "NOGROUP",
                    StringComparison.OrdinalIgnoreCase))
            {
                await EnsureConsumerGroupAsync(
                    database,
                    streamName,
                    cancellationToken);

                continue;
            }
            catch (RedisServerException ex)
            {
                logger.LogWarning(
                    ex,
                    "XAUTOCLAIM failed. Stream={Stream}, Group={Group}",
                    streamName,
                    ConsumerGroupName);

                continue;
            }

            if (result.IsNull ||
                !IsAggregateResult(result))
            {
                logger.LogWarning(
                    "Unexpected XAUTOCLAIM response type. Stream={Stream}, Resp2Type={Resp2Type}, Resp3Type={Resp3Type}, Length={Length}, Value={Value}",
                    streamName,
                    result.Resp2Type,
                    result.Resp3Type,
                    result.Length,
                    result);

                continue;
            }

            if (result.Length < 2)
            {
                continue;
            }

            var nextCursor =
                (string?)result[0];

            if (string.IsNullOrEmpty(nextCursor) ||
                nextCursor == "0-0")
            {
                _claimCursors.Remove(
                    streamName);
            }
            else
            {
                _claimCursors[streamName] =
                    nextCursor;
            }

            var entriesResult =
                result[1];

            if (entriesResult.IsNull ||
                !IsAggregateResult(entriesResult))
            {
                continue;
            }

            if (entriesResult.Length == 0)
            {
                continue;
            }

            var acknowledgementIds =
                new List<RedisValue>(
                    entriesResult.Length);

            for (var i = 0;
                 i < entriesResult.Length;
                 i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var entryResult =
                    entriesResult[i];

                var entry =
                    ParseStreamEntry(entryResult);

                if (entry.Id.IsNullOrEmpty)
                {
                    continue;
                }

                var success = false;

                try
                {
                    success =
                        await ProcessEventAsync(
                            eventName,
                            entry,
                            cancellationToken);
                }
                catch (OperationCanceledException)
                    when (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    logger.LogWarning(
                        ex,
                        "Error processing reclaimed event. EventType={EventType}, MessageId={MessageId}",
                        eventName,
                        entry.Id);
                }

                if (success)
                {
                    acknowledgementIds.Add(
                        entry.Id);
                }
            }

            if (acknowledgementIds.Count > 0)
            {
                await AcknowledgeAsync(
                    streamName,
                    acknowledgementIds);
            }
        }
    }

    private async Task CleanupLoopAsync(
        CancellationToken cancellationToken)
    {
        using var timer =
            new PeriodicTimer(
                _options.CleanupInterval);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await timer.WaitForNextTickAsync(
                    cancellationToken);

                await CleanupStreamsAsync(
                    cancellationToken);
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            catch (RedisConnectionException ex)
            {
                logger.LogWarning(
                    ex,
                    "Redis connection error in Stream cleanup loop.");

                await DelayReconnectAsync(
                    cancellationToken);
            }
            catch (RedisTimeoutException ex)
            {
                logger.LogWarning(
                    ex,
                    "Redis timeout in Stream cleanup loop.");

                await DelayReconnectAsync(
                    cancellationToken);
            }
            catch (RedisServerException ex)
            {
                logger.LogWarning(
                    ex,
                    "Redis server error in Stream cleanup loop.");

                await DelayReconnectAsync(
                    cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Unexpected error in Redis Stream cleanup loop.");

                await DelayReconnectAsync(
                    cancellationToken);
            }
        }
    }

    private async Task CleanupStreamsAsync(
        CancellationToken cancellationToken)
    {
        if (_options.Retention <= TimeSpan.Zero)
        {
            return;
        }

        var database =
            _database;

        if (database is null)
        {
            return;
        }

        var minimumTimestamp =
            DateTimeOffset.UtcNow
                .Subtract(_options.Retention)
                .ToUnixTimeMilliseconds();

        var minimumId =
            $"{minimumTimestamp}-0";

        foreach (var eventType in
                 _subscriptionInfo.EventTypes.Keys)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var streamName =
                GetStreamName(eventType);

            try
            {
                await database.ExecuteAsync(
                    "XTRIM",
                    streamName,
                    "MINID",
                    "~",
                    minimumId,
                    "ACKED");
            }
            catch (RedisServerException ex)
            {
                logger.LogWarning(
                    ex,
                    "Failed to trim Redis Stream. Stream={Stream}, MinimumId={MinimumId}",
                    streamName,
                    minimumId);
            }
        }
    }

    private static StreamEntry ParseStreamEntry(
        RedisResult result)
    {
        if (result.IsNull ||
            !IsAggregateResult(result) ||
            result.Length < 2)
        {
            return default;
        }

        var id =
            (RedisValue)result[0];

        var fieldValues =
            result[1];

        if (fieldValues.IsNull ||
            !IsAggregateResult(fieldValues))
        {
            return new StreamEntry(
                id,
                []);
        }

        var fieldCount =
            fieldValues.Length / 2;

        if (fieldCount == 0)
        {
            return new StreamEntry(
                id,
                []);
        }

        var entries =
            new NameValueEntry[fieldCount];

        var index = 0;

        for (var i = 0;
             i + 1 < fieldValues.Length;
             i += 2)
        {
            entries[index++] =
                new NameValueEntry(
                    (RedisValue)fieldValues[i],
                    (RedisValue)fieldValues[i + 1]);
        }

        return new StreamEntry(
            id,
            entries);
    }

    private static RedisValue GetField(
        NameValueEntry[] values,
        RedisValue fieldName)
    {
        foreach (var entry in values)
        {
            if (entry.Name == fieldName)
            {
                return entry.Value;
            }
        }

        return RedisValue.Null;
    }

    private static bool IsAggregateResult(
        RedisResult result)
    {
        return result.Resp3Type is
            ResultType.Array or
            ResultType.Map or
            ResultType.Set;
    }

    private string GetStreamName(
        string eventType)
    {
        return $"{_options.StreamPrefix}:{eventType}";
    }

    private void BuildStreamMap()
    {
        var dictionary =
            new Dictionary<string, string>(
                StringComparer.Ordinal);

        foreach (var eventType in
                 _subscriptionInfo.EventTypes.Keys)
        {
            dictionary.Add(
                GetStreamName(eventType),
                eventType);
        }

        _streamToEventType =
            dictionary;
    }

    private async Task DelayReconnectAsync(
        CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(
                1000,
                cancellationToken);
        }
        catch (OperationCanceledException)
        {
        }
    }

    public async Task StopAsync(
        CancellationToken cancellationToken)
    {
        if (!_started)
        {
            return;
        }

        _disposeCts.Cancel();

        await CloseConsumerConnectionAsync();

        var tasks =
            new[]
            {
                _consumerTask,
                _claimTask,
                _cleanupTask
            };

        foreach (var task in tasks)
        {
            if (task is null)
            {
                continue;
            }

            try
            {
                await task.WaitAsync(
                    cancellationToken);
            }
            catch (OperationCanceledException)
            {
            }
        }

        await CloseConnectionsAsync();

        _consumerTask = null;
        _claimTask = null;
        _cleanupTask = null;

        _started = false;
    }

    private async Task CloseConsumerConnectionAsync()
    {
        var consumerRedis =
            _consumerRedis;

        if (consumerRedis is null)
        {
            return;
        }

        try
        {
            await consumerRedis.CloseAsync(
                allowCommandsToComplete: false);
        }
        catch (Exception ex)
        {
            logger.LogDebug(
                ex,
                "Error while closing Redis consumer connection.");
        }
    }

    private async Task CloseConnectionsAsync()
    {
        var consumerRedis =
            _consumerRedis;

        if (consumerRedis is not null)
        {
            try
            {
                await consumerRedis.CloseAsync(
                    allowCommandsToComplete: false);
            }
            catch (Exception ex)
            {
                logger.LogDebug(
                    ex,
                    "Error while closing Redis consumer connection.");
            }

            _consumerRedis = null;
            _consumerDatabase = null;
        }

        var redis =
            _redis;

        if (redis is not null)
        {
            try
            {
                await redis.CloseAsync(
                    allowCommandsToComplete: false);
            }
            catch (Exception ex)
            {
                logger.LogDebug(
                    ex,
                    "Error while closing Redis connection.");
            }

            _redis = null;
            _database = null;
        }
    }

    private static ResiliencePipeline CreateResiliencePipeline(
        int retryCount)
    {
        return new ResiliencePipelineBuilder()
            .AddRetry(
                new RetryStrategyOptions
                {
                    MaxRetryAttempts = retryCount,
                    Delay =
                        TimeSpan.FromMilliseconds(200),
                    BackoffType =
                        DelayBackoffType.Exponential,
                    UseJitter = true,
                    ShouldHandle =
                        new PredicateBuilder()
                            .Handle<RedisConnectionException>()
                            .Handle<RedisTimeoutException>()
                })
            .Build();
    }

    public void Dispose()
    {
        if (!_disposeCts.IsCancellationRequested)
        {
            _disposeCts.Cancel();
        }

        _consumerRedis?.Dispose();
        _redis?.Dispose();

        _consumerRedis = null;
        _redis = null;

        _consumerDatabase = null;
        _database = null;

        _disposeCts.Dispose();
    }
}