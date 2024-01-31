using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace MyTelegram.Services.Services;

public class AckCacheService : IAckCacheService
{
    private readonly ConcurrentDictionary<long, AckCacheItem> _msgIdToPtsDict = new();
    private readonly ConcurrentDictionary<long, long> _msgIdToReqMsgIdDict = new();
    private readonly ConcurrentDictionary<long, AckCacheItem> _rpcReqMsgIdToPtsDict = new();
    private readonly IScheduleAppService _scheduleAppService;
    private readonly int _timeoutSeconds = 60 * 10;//10minute

    public AckCacheService(IScheduleAppService scheduleAppService)
    {
        _scheduleAppService = scheduleAppService;
    }

    public void AddRpcMsgIdToCache(long msgId,
        long reqMsgId)
    {
        //Console.WriteLine($"Add Rpc msgId to cache,msgId:{msgId} reqMsgId:{reqMsgId}");
        _msgIdToReqMsgIdDict.TryAdd(msgId, reqMsgId);
        _scheduleAppService.Execute(() => _msgIdToReqMsgIdDict.TryRemove(msgId, out _), TimeSpan.FromSeconds(50));
    }

    public Task AddRpcPtsToCacheAsync(long reqMsgId,
        int pts,
        long globalSeqNo,
        Peer toPeer)
    {
        //Console.WriteLine($"AddRpcPtsToCacheAsync,reqMsgId:{reqMsgId} pts:{pts} globalSeqNo:{globalSeqNo} toPeer:{toPeer}");

        _rpcReqMsgIdToPtsDict.TryAdd(reqMsgId, new AckCacheItem(pts, globalSeqNo, toPeer));
        _scheduleAppService.Execute(() =>
            {
                _rpcReqMsgIdToPtsDict.TryRemove(reqMsgId, out _);
            },
            TimeSpan.FromSeconds(_timeoutSeconds));
        return Task.CompletedTask;
    }

    public bool TryGetRpcPtsCache(long msgId,
        [NotNullWhen(true)] out AckCacheItem? ackRpcCacheItem)
    {
        if (_msgIdToReqMsgIdDict.TryRemove(msgId, out var reqMsgId))
        {
            return _rpcReqMsgIdToPtsDict.TryRemove(reqMsgId, out ackRpcCacheItem);
        }

        ackRpcCacheItem = null;
        return false;
    }

    public Task AddMsgIdToCacheAsync(long msgId,
        int pts,
        long globalSeqNo,
        Peer toPeer)
    {
        //Console.WriteLine($"##########AddMsgIdToCacheAsync msgId {msgId} pts {pts} globalSeqNo {globalSeqNo} toPeer:{toPeer}");
        _msgIdToPtsDict.TryAdd(msgId, new AckCacheItem(pts, globalSeqNo, toPeer));
        _scheduleAppService.Execute(() =>
            {
                _msgIdToPtsDict.TryRemove(msgId, out _);
            },
            TimeSpan.FromSeconds(_timeoutSeconds));
        return Task.CompletedTask;
    }

    public bool TryGetPts(long msgId,
        [NotNullWhen(true)] out AckCacheItem? ackCacheItem)
    {
        return _msgIdToPtsDict.TryRemove(msgId, out ackCacheItem);
    }
}