namespace MyTelegram.MessengerServer.Services;

public record AckCacheItem(int Pts,
    long GlobalSeqNo,
    Peer ToPeer);