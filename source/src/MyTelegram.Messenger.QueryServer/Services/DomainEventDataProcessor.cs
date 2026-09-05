using MyTelegram.Messenger.Services.Caching;

namespace MyTelegram.Messenger.QueryServer.Services;

public class DomainEventDataProcessor(IDispatchToEventSubscribers dispatchToEventSubscribers,
    ICachedReadModelUpdater cachedReadModelUpdater,
    IChatEventCacheHelper chatEventCacheHelper,
    ILogger<DomainEventDataProcessor> logger) : IDataProcessor<IDomainEvent>, ITransientDependency
{
    public async Task ProcessAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var maxMillSeconds = 500;
        var sw = Stopwatch.StartNew();

        var aggregateEvent = domainEvent.GetAggregateEvent();

        switch (aggregateEvent)
        {
            case ChannelCreatedEvent channelCreatedEvent:
                chatEventCacheHelper.Add(channelCreatedEvent);
                break;
        }

        await cachedReadModelUpdater.UpdateAsync([domainEvent], cancellationToken);
        await dispatchToEventSubscribers.DispatchToSynchronousSubscribersAsync([domainEvent],
            cancellationToken);
        await dispatchToEventSubscribers.DispatchToAsynchronousSubscribersAsync(domainEvent, cancellationToken);
        sw.Stop();

        if (sw.Elapsed.TotalMilliseconds > maxMillSeconds)
        {
            logger.LogInformation("Process domain event '{DomainEvent}' is too slow, time {Timespan}ms",
                domainEvent.GetAggregateEvent().GetType().Name,
                sw.Elapsed);
        }
    }
}