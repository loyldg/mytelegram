namespace MyTelegram.EventBus.Redis;

//public sealed class EventBusRedisOptions
//{
//    public string ConnectionString { get; set; } = "localhost:6379";

//    public string StreamPrefix { get; set; } = "mytelegram:eventbus";

//    /// <summary>
//    /// Equivalent to RabbitMQ queue name.
//    /// Multiple instances using the same ClientName share messages.
//    /// </summary>
//    public string ClientName { get; set; } = "default";

//    /// <summary>
//    /// Maximum number of messages returned by XREADGROUP.
//    /// </summary>
//    public int BatchSize { get; set; } = 128;

//    /// <summary>
//    /// XREADGROUP BLOCK timeout.
//    /// </summary>
//    public int BlockMilliseconds { get; set; } = 1000;

//    /// <summary>
//    /// Minimum idle time before a pending message can be reclaimed.
//    /// </summary>
//    public int ClaimMinIdleMilliseconds { get; set; } = 30000;

//    /// <summary>
//    /// How often XAUTOCLAIM is executed.
//    /// </summary>
//    public int ClaimIntervalMilliseconds { get; set; } = 5000;

//    /// <summary>
//    /// Maximum stream length.
//    /// 0 means unlimited.
//    /// </summary>
//    public int MaxStreamLength { get; set; }

//    public int RetryCount { get; set; } = 3;
//}

public sealed class EventBusRedisOptions
{
    public string ConnectionString { get; set; } = "localhost:6379";

    public string StreamPrefix { get; set; } = "mytelegram:eventbus";

    public int MaxStreamLength { get; set; } = 10000;

    public int BlockMilliseconds { get; set; } = 5000;

    public int BatchSize { get; set; } = 100;

    public int RetryCount { get; set; } = 3;

    public int ClaimIntervalMilliseconds { get; set; } = 10000;

    public int ClaimMinIdleMilliseconds { get; set; } = 30000;

    public int ClaimBatchSize { get; set; } = 100;

    public long StreamRetentionMilliseconds { get; set; } = 120000;

    public int StreamCleanupIntervalMilliseconds { get; set; } = 30000;

    public TimeSpan Retention { get; set; } =
        TimeSpan.FromSeconds(5);

    public TimeSpan CleanupInterval { get; set; } =
        TimeSpan.FromSeconds(10);
}