using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace MyTelegram.Services.Services;

public class RpcResultCacheAppService(ILogger<RpcResultCacheAppService> logger, IScheduleAppService scheduleAppService)
    : IRpcResultCacheAppService, ISingletonDependency
{
    private readonly TimeSpan _cacheExpireTimeSpan =
        TimeSpan.FromSeconds(MyTelegramConsts.MaxTimeDiffSeconds);

    private readonly int _maxRpcResultCacheCount = 200000;
    private readonly ConcurrentDictionary<long, IObject> _rpcResults = new();
    private long _removedCount;
    private long _totalCount;

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

        scheduleAppService.Execute(() =>
        {
            _rpcResults.TryRemove(reqMsgId, out _);
            Interlocked.Increment(ref _removedCount);
#if DEBUG
            logger.LogDebug(
                "Remove rpc result cache, current cache count is {CacheCount}, total cached count {TotalCachedCount}",
                _rpcResults.Count,
                _totalCount);
#endif
            if (_removedCount % 1000 == 0)
            {
                logger.LogInformation(
                    "Remove rpc result cache, current cache count is {CacheCount}, total cached count {TotalCachedCount}",
                    _rpcResults.Count,
                    _totalCount);
            }
        }, _cacheExpireTimeSpan);

        Interlocked.Increment(ref _totalCount);
        return _rpcResults.TryAdd(reqMsgId, rpcResult);
    }
}