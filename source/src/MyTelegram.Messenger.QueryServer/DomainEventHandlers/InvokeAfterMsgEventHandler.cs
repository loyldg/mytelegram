using IInvokeAfterMsgProcessor = MyTelegram.Services.Services.IInvokeAfterMsgProcessor;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class InvokeAfterMsgEventHandler(IInvokeAfterMsgProcessor invokeAfterMsgProcessor) : ISubscribeSynchronousToAll
{
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

            invokeAfterMsgProcessor.AddToRecentMessageIdList(reqMsgId);
            await invokeAfterMsgProcessor.HandleAsync(reqMsgId);
        }
    }
}
