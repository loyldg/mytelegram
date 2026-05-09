namespace MyTelegram.Messenger.QueryServer.EventHandlers;

public class DistributedDomainEventHandler(
    IEventJsonSerializer eventJsonSerializer,
    ILogger<DistributedDomainEventHandler> logger,
    IMessageQueueProcessor<IDomainEvent> messageQueueProcessor )
    : IEventHandler<DomainEventMessage>, ITransientDependency
{
    public Task HandleEventAsync(DomainEventMessage eventData)
    {
        var domainEvent = eventJsonSerializer.Deserialize(eventData.Message, new Metadata(eventData.Headers));

        var queueKey = 0L;
        var aggregateEvent = domainEvent.GetAggregateEvent();
        if (aggregateEvent is IHasRequestInfo hasRequestInfo)
        {
            queueKey = hasRequestInfo.RequestInfo.PermAuthKeyId;

            var totalMilliseconds =
                DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - hasRequestInfo.RequestInfo.Date;
            const int maxMillSeconds = 500;

            if (totalMilliseconds > maxMillSeconds)
            {
                logger.LogInformation(
                    "Process domain event '{DomainEvent}' is too slow, time: {Timespan}ms, reqMsgId: {ReqMsgId}",
                    domainEvent.GetAggregateEvent().GetType().Name,
                    totalMilliseconds,
                    hasRequestInfo.RequestInfo.ReqMsgId
                );
            }
        }
        messageQueueProcessor.Enqueue(domainEvent, queueKey);

        return Task.CompletedTask;
    }
}