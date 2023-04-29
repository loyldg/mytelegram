namespace MyTelegram.MessengerServer.DomainEventHandlers;

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
                IHasRequestMessageId hasRequestMessageId => hasRequestMessageId.ReqMsgId,
                IHasRequestInfo hasRequestInfo => hasRequestInfo.RequestInfo.ReqMsgId,
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