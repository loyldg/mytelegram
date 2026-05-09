using MyTelegram.Messenger.Services.Caching;

namespace MyTelegram.Messenger.CommandServer.Services;

public class DomainEventsDataProcessor(
    IEventBus eventBus,
    IDomainEventMessageFactory domainEventMessageFactory,
    ICachedReadModelUpdater cachedReadModelUpdater,
    ILogger<DomainEventsDataProcessor> logger) : IDataProcessor<IReadOnlyCollection<IDomainEvent>>, ITransientDependency
{
    public async Task ProcessAsync(IReadOnlyCollection<IDomainEvent> domainEvents)
    {
        await cachedReadModelUpdater.UpdateAsync(domainEvents, CancellationToken.None);

        foreach (var domainEvent in domainEvents)
        {
            var aggregateEvent = domainEvent.GetAggregateEvent();
            if (aggregateEvent is IHasRequestInfo requestInfo)
            {
                var totalMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - requestInfo.RequestInfo.Date;
                if (totalMilliseconds > 500)
                {
                    logger.LogDebug("Process domain event '{DomainEvent}' is too slow, timespan: {Timespan}ms, reqMsgId: {ReqMsgId}",
                        domainEvent.GetAggregateEvent().GetType().Name,
                        totalMilliseconds,
                        requestInfo.RequestInfo.ReqMsgId);
                }
            }

            var message = domainEventMessageFactory.CreateDomainEventMessage(domainEvent);

            await eventBus.PublishAsync(message);
        }
    }
}