using IInvokeAfterMsgProcessor = MyTelegram.Services.Services.IInvokeAfterMsgProcessor;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class InvokeAfterMsgEventHandler : ISubscribeSynchronousToAll
{
    private readonly IInvokeAfterMsgProcessor _invokeAfterMsgProcessor;

    public InvokeAfterMsgEventHandler(IInvokeAfterMsgProcessor invokeAfterMsgProcessor)
    {
        _invokeAfterMsgProcessor = invokeAfterMsgProcessor;
    }

    public async Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents,
        CancellationToken cancellationToken)
    {
        foreach (var domainEvent in domainEvents)
        {
            var aggregateEvent = domainEvent.GetAggregateEvent();
            var reqMsgId = aggregateEvent switch
            {
                IHasRequestInfo hasRequestInfo => hasRequestInfo.RequestInfo.AddRequestIdToCache ? hasRequestInfo.RequestInfo.ReqMsgId : 0,
                _ => 0L
            };
            if (reqMsgId == 0)
            {
                continue;
            }

            _invokeAfterMsgProcessor.AddToRecentMessageIdList(reqMsgId);
            await _invokeAfterMsgProcessor.HandleAsync(reqMsgId);
        }
    }
}
