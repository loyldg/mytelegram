namespace MyTelegram.MessengerServer.Services.Caching;

public class InvokeAfterMsgProcessor : IInvokeAfterMsgProcessor //, ISingletonDependency
{
    private readonly IHandlerHelper _handlerHelper;

    //private readonly ConcurrentDictionary<long, int> _recentMessageIds = new();
    private readonly CircularBuffer<long> _recentMessageIds = new(50000);
    private readonly ConcurrentDictionary<long, InvokeAfterMsgItem> _requests = new();
    private readonly System.Threading.Channels.Channel<long> _completedReqMsgIds = Channel.CreateUnbounded<long>();
    public InvokeAfterMsgProcessor(IHandlerHelper handlerHelper)
    {
        _handlerHelper = handlerHelper;
    }

    public void AddToRecentMessageIdList(long messageId)
    {
        //_recentMessageIds.TryAdd(messageId, DateTime.UtcNow.ToTimestamp());
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

    public ValueTask AddCompletedReqMsgIdAsync(long reqMsgId)
    {
        //_completedReqMsgIds.Writer.TryWrite(reqMsgId);
        return _completedReqMsgIds.Writer.WriteAsync(reqMsgId);
    }

    public async Task ProcessAsync()
    {
        while (await _completedReqMsgIds.Reader.WaitToReadAsync().ConfigureAwait(false))
        {
            if (_completedReqMsgIds.Reader.TryRead(out var reqMsgId))
            {
                try
                {
                    await HandleAsync(reqMsgId).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }

    public Task HandleAsync(long reqMsgId)
    {
        if (_requests.TryGetValue(reqMsgId, out var item))
        {
            if (!_handlerHelper.TryGetHandler(item.Query.ConstructorId, out var handler))
            {
                throw new NotImplementedException($"Not supported query:{item.Query.ConstructorId:x2}");
            }
            // Console.WriteLine($">>>>>> Handle invoke after msg:{reqMsgId}");
            return handler.HandleAsync(item.Input, item.Query);
        }
        else
        {
            // Console.WriteLine($"XXXXXX ReqMsgId:{reqMsgId} not find invoke after msg");
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

        //Console.WriteLine($"Handle exists reqMsgId:{input.ReqMsgId}");
        return handler.HandleAsync(input, query);
    }
}