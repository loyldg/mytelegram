namespace MyTelegram.MessengerServer.Services.Caching;

public class ResponseCacheAppService : IResponseCacheAppService //, ISingletonDependency
{
    private readonly ConcurrentDictionary<long, List<IObject>> _responseDict = new();

    public int AddToCache(long reqMsgId,
        IObject response)
    {
        if (!_responseDict.TryGetValue(reqMsgId, out var responseList))
        {
            responseList = new List<IObject>();
            _responseDict.TryAdd(reqMsgId, responseList);
        }

        responseList.Add(response);

        return responseList.Count;
    }

    public bool TryRemoveResponseList(long reqMsgId,
        [NotNullWhen(true)] out List<IObject>? responseList)
    {
        if (_responseDict.TryRemove(reqMsgId, out responseList))
        {
            return true;
        }

        return false;
    }
}