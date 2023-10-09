using System.Diagnostics.CodeAnalysis;

namespace MyTelegram.Services.Services;

public interface IAckCacheService
{
    Task AddMsgIdToCacheAsync(long msgId,
        int pts,
        long globalSeqNo,
        Peer toPeer);

    void AddRpcMsgIdToCache(long msgId,
        long reqMsgId);

    Task AddRpcPtsToCacheAsync(long reqMsgId,
        int pts,
        long globalSeqNo,
        Peer toPeer);

    bool TryGetPts(long msgId,
        [NotNullWhen(true)] out AckCacheItem? ackCacheItem);

    bool TryGetRpcPtsCache(long msgId,
        [NotNullWhen(true)] out AckCacheItem? ackRpcCacheItem);
}