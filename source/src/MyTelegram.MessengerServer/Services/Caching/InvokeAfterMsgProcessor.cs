namespace MyTelegram.MessengerServer.Services.Caching;

public class InvokeAfterMsgProcessor : IInvokeAfterMsgProcessor
{
    private readonly IHandlerHelper _handlerHelper;

    //private readonly ConcurrentDictionary<long, int> _recentMessageIds = new();
    private readonly CircularBuffer<long> _recentMessageIds = new(50000);
    private readonly ConcurrentDictionary<long, InvokeAfterMsgItem> _requests = new();

    public InvokeAfterMsgProcessor(IHandlerHelper handlerHelper)
    {
        _handlerHelper = handlerHelper;
    }

    public void AddToRecentMessageIdList(long messageId)
    {
        _recentMessageIds.Put(messageId);
    }

    public bool ExistsInRecentMessageId(long messageId)
    {
        return _recentMessageIds.Contains(messageId);
    }

    public void Enqueue(long reqMsgId,
        IRequestInput input,
        IObject query)
    {
        _requests.TryAdd(reqMsgId, new InvokeAfterMsgItem(input, query));
    }

    public Task HandleAsync(long reqMsgId)
    {
        if (_requests.TryGetValue(reqMsgId, out var item))
        {
            if (!_handlerHelper.TryGetHandler(item.Query.ConstructorId, out var handler))
            {
                throw new NotImplementedException($"Not supported query:{item.Query.ConstructorId:x2}");
            }

            return handler.HandleAsync(item.Input, item.Query);
        }

        return Task.CompletedTask;
    }

    public Task<IObject> HandleAsync(IRequestInput input,
        IObject query)
    {
        if (!_handlerHelper.TryGetHandler(query.ConstructorId, out var handler))
        {
            throw new NotSupportedException($"Not supported query:{query.ConstructorId:x2}");
        }

        return handler.HandleAsync(input, query);
    }
}