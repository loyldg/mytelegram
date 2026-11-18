namespace MyTelegram.AuthServer.Services;

public interface IHandshakeRateLimiter
{
    /// <summary>
    /// Consumes one token for <paramref name="key"/> (the client IP). Returns false when the limit is exceeded.
    /// </summary>
    bool TryAcquire(string key);
}

/// <summary>
/// In-memory token bucket per client IP. Each auth server instance limits independently.
/// </summary>
public class HandshakeRateLimiter(
    IOptionsMonitor<MyTelegramAuthServerOptions> options,
    ILogger<HandshakeRateLimiter> logger) : IHandshakeRateLimiter, ISingletonDependency
{
    private const int CleanupThreshold = 100_000;
    private static readonly TimeSpan IdleTimeout = TimeSpan.FromMinutes(10);

    private readonly ConcurrentDictionary<string, Bucket> _buckets = new();
    private long _lastCleanupTicks = Environment.TickCount64;

    public bool TryAcquire(string key)
    {
        var limit = options.CurrentValue.HandshakeRateLimit;
        if (!limit.Enabled || limit.Capacity <= 0 || limit.TokensPerMinute <= 0)
        {
            return true;
        }

        var now = Environment.TickCount64;
        CleanupIfNeeded(now);

        var bucket = _buckets.GetOrAdd(key, static (_, capacity) => new Bucket(capacity, Environment.TickCount64), limit.Capacity);
        lock (bucket)
        {
            var elapsedMs = now - bucket.LastRefillTicks;
            if (elapsedMs > 0)
            {
                bucket.Tokens = Math.Min(limit.Capacity, bucket.Tokens + elapsedMs * limit.TokensPerMinute / 60000.0);
                bucket.LastRefillTicks = now;
            }

            if (bucket.Tokens >= 1)
            {
                bucket.Tokens -= 1;
                bucket.Limited = false;
                return true;
            }

            if (!bucket.Limited)
            {
                // Log once per throttling period to avoid flooding the log during an attack.
                bucket.Limited = true;
                logger.LogWarning("Handshake rate limit exceeded, requests from {ClientKey} are dropped", key);
            }

            return false;
        }
    }

    private void CleanupIfNeeded(long now)
    {
        if (_buckets.Count < CleanupThreshold)
        {
            return;
        }

        var lastCleanup = Interlocked.Read(ref _lastCleanupTicks);
        if (now - lastCleanup < 10_000 ||
            Interlocked.CompareExchange(ref _lastCleanupTicks, now, lastCleanup) != lastCleanup)
        {
            return;
        }

        var idleMs = (long)IdleTimeout.TotalMilliseconds;
        foreach (var item in _buckets)
        {
            if (now - item.Value.LastRefillTicks > idleMs)
            {
                _buckets.TryRemove(item.Key, out _);
            }
        }
    }

    private sealed class Bucket(double tokens, long lastRefillTicks)
    {
        public double Tokens { get; set; } = tokens;
        public long LastRefillTicks { get; set; } = lastRefillTicks;
        public bool Limited { get; set; }
    }
}
