using IInvokeAfterMsgProcessor = MyTelegram.Services.Services.IInvokeAfterMsgProcessor;

namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;

public class InvokeAfterMsgEventHandler : ISubscribeSynchronousToAll
{
    private readonly IInvokeAfterMsgProcessor _invokeAfterMsgProcessor;
    private readonly ILogger<InvokeAfterMsgEventHandler> _logger;
    public InvokeAfterMsgEventHandler(IInvokeAfterMsgProcessor invokeAfterMsgProcessor, ILogger<InvokeAfterMsgEventHandler> logger)
    {
        _invokeAfterMsgProcessor = invokeAfterMsgProcessor;
        _logger = logger;
    }

    public async Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents,
        CancellationToken cancellationToken)
    {
        foreach (var domainEvent in domainEvents)
        {
            var aggregateEvent = domainEvent.GetAggregateEvent();
            //var reqMsgId = aggregateEvent switch
            //{
            //    IHasRequestInfo hasRequestInfo => hasRequestInfo.RequestInfo.AddRequestIdToCache ? hasRequestInfo.RequestInfo.ReqMsgId : 0,
            //    _ => 0L
            //};
            var reqMsgId = 0L;
            switch (aggregateEvent)
            {
                case IHasRequestInfo requestInfo:
                    if (requestInfo.RequestInfo.AddRequestIdToCache)
                    {
                        reqMsgId = requestInfo.RequestInfo.ReqMsgId;
                    }

                    var timespan = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - requestInfo.RequestInfo.Date;
                    if (timespan > 500)
                    {
                        //Console.WriteLine($"{aggregateEvent.GetType().Name} mill seconds:{timespan} ReqMsgId:{requestInfo.RequestInfo.ReqMsgId}");
                        _logger.LogWarning("{Name} mill seconds:{Timespan} reqMsgId:{ReqMsgId}", aggregateEvent.GetType().Name, timespan, requestInfo.RequestInfo.ReqMsgId);
                    }

                    break;
            }

            if (reqMsgId == 0)
            {
                continue;
            }

            _invokeAfterMsgProcessor.AddToRecentMessageIdList(reqMsgId);
            await _invokeAfterMsgProcessor.HandleAsync(reqMsgId);
        }
    }
}
