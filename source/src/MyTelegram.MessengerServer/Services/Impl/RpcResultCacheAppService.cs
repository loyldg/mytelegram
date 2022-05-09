namespace MyTelegram.MessengerServer.Services.Impl;

public class RpcResultCacheAppService : IRpcResultCacheAppService
{
    //private readonly IMemoryCache _memoryCache;
    private readonly TimeSpan _cacheExpireTimeSpan =
        TimeSpan.FromSeconds(MyTelegramServerDomainConsts.MaxTimeDiffSeconds);

    private readonly HashedWheelTimer _hashedWheelTimer = new();
    private readonly ILogger<RpcResultCacheAppService> _logger;
    private readonly int _maxRpcResultCacheCount = 200000;
    private readonly ConcurrentDictionary<long, IObject> _rpcResults = new();
    private long _removedCount;
    private long _totalCount;

    public RpcResultCacheAppService(ILogger<RpcResultCacheAppService> logger)
    {
        _logger = logger;
    }

    public bool TryGetRpcResult(long userId,
        long reqMsgId,
        [NotNullWhen(true)] out IObject? rpcResult)
    {
        return _rpcResults.TryGetValue(reqMsgId, out rpcResult);
    }

    public bool TryAdd(long userId,
        long reqMsgId,
        IObject rpcResult)
    {
        if (_rpcResults.Count > _maxRpcResultCacheCount)
        {
            return false;
        }

        _hashedWheelTimer.NewTimeout(new ActionTimeTask(() =>
            {
                _rpcResults.TryRemove(reqMsgId, out _);
                Interlocked.Increment(ref _removedCount);
#if DEBUG
                _logger.LogDebug(
                    "Remove rpc result cache,current cache count is {CacheCount},total cached count {TotalCachedCount}",
                    _rpcResults.Count,
                    _totalCount);
#endif
                if (_removedCount % 1000 == 0)
                {
                    _logger.LogInformation(
                        "Remove rpc result cache,current cache count is {CacheCount},total cached count {TotalCachedCount}",
                        _rpcResults.Count,
                        _totalCount);
                }
            }),
            _cacheExpireTimeSpan);
        Interlocked.Increment(ref _totalCount);
        return _rpcResults.TryAdd(reqMsgId, rpcResult);
    }
}