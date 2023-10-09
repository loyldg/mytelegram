namespace MyTelegram.Services.Services;

public record AckCacheItem(int Pts,
    long GlobalSeqNo,
    Peer ToPeer);