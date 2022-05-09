namespace MyTelegram.MessengerServer.Services;
//public interface IScheduleAppService
//{
//    long Execute(Action action,
//        TimeSpan timeSpan);
//}

public record AckCacheItem(int Pts,
    long GlobalSeqNo,
    Peer ToPeer);

//public record AckRpcCacheItem(long ReqMsgId,
//    int Pts,
//    long GlobalSeqNo,
//    Peer ToPeer);