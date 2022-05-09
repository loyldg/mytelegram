namespace MyTelegram.MessengerServer.Services.Interfaces;

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

    bool TryRemovePts(long msgId,
        [NotNullWhen(true)] out AckCacheItem? ackCacheItem);

    bool TryRemoveRpcPtsCache(long msgId,
        [NotNullWhen(true)] out AckCacheItem? ackRpcCacheItem);
}