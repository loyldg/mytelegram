namespace MyTelegram.MessengerServer.Services.Caching;

public interface IResponseCacheAppService
{
    int AddToCache(long reqMsgId,
        IObject response);

    bool TryRemoveResponseList(long reqMsgId,
        [NotNullWhen(true)] out List<IObject>? responseList);
}