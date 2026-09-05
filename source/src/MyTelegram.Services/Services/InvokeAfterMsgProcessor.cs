using EventFlow.Core;
using System.Collections.Concurrent;
using System.Threading.Channels;

namespace MyTelegram.Services.Services;

public class InvokeAfterMsgProcessor(IHandlerHelper handlerHelper, ILogger<InvokeAfterMsgProcessor> logger) : IInvokeAfterMsgProcessor
    , ISingletonDependency
{
    private readonly CircularBuffer<long> _recentMessageIds = new(100000);
    private readonly ConcurrentDictionary<long, InvokeAfterMsgItem> _pendingRequests = new();
    private readonly Channel<long> _completedReqMsgIds = Channel.CreateUnbounded<long>();

    public void AddToRecentMessageIdList(long messageId)
    {
        _recentMessageIds.Put(messageId);
    }

    public bool ExistsInRecentMessageId(long messageId)
    {
        return _recentMessageIds.Contains(messageId);
    }

    public void Enqueue(long invokeAfterMsgId,
        IRequestInput input,
        IObject query)
    {
        _pendingRequests.TryAdd(invokeAfterMsgId, new InvokeAfterMsgItem(input, query));
    }

    public ValueTask AddCompletedReqMsgIdAsync(long reqMsgId)
    {
        _recentMessageIds.Put(reqMsgId);
        return _completedReqMsgIds.Writer.WriteAsync(reqMsgId);
    }

    public async Task ProcessAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            while (await _completedReqMsgIds.Reader
                       .WaitToReadAsync(cancellationToken)
                       .ConfigureAwait(false))
            {
                while (_completedReqMsgIds.Reader.TryRead(
                           out var reqMsgId))
                {
                    try
                    {
                        await HandleAsync(
                            reqMsgId);
                    }
                    catch (OperationCanceledException)
                        when (cancellationToken.IsCancellationRequested)
                    {
                        // Normal shutdown.
                        return;
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(
                            ex,
                            "InvokeAfterMsg failed.");
                    }
                }
            }
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {
            // Normal shutdown.
        }
    }

    public Task HandleAsync(long reqMsgId)
    {
        if (_pendingRequests.TryRemove(reqMsgId, out var item))
        {
            if (!handlerHelper.TryGetHandler(item.Query.ConstructorId, out var handler))
            {
                throw new NotImplementedException($"Not supported query: {item.Query.ConstructorId:x2}");
            }

            return handler.HandleAsync(item.Input, item.Query);
        }

        return Task.CompletedTask;
    }

    public Task<IObject> HandleAsync(IRequestInput input,
        IObject query)
    {
        if (!handlerHelper.TryGetHandler(query.ConstructorId, out var handler))
        {
            throw new NotSupportedException($"Not supported query:{query.ConstructorId:x2}");
        }

        return handler.HandleAsync(input, query);
    }
}