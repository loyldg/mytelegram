using MyTelegram.Messenger.Services.Caching;

namespace MyTelegram.Messenger.QueryServer.EventHandlers;

public class DistributedDomainEventHandler(
    IEventJsonSerializer eventJsonSerializer,
    IDispatchToEventSubscribers dispatchToEventSubscribers,
    ILogger<DistributedDomainEventHandler> logger,
    ICachedReadModelUpdater cachedReadModelUpdater,
    IChatEventCacheHelper chatEventCacheHelper)
    : IEventHandler<DomainEventMessage>, ITransientDependency
{
    public async Task HandleEventAsync(DomainEventMessage eventData)
    {
        var maxMillSeconds = 500;
        var sw = Stopwatch.StartNew();
        var domainEvent = eventJsonSerializer.Deserialize(eventData.Message, new Metadata(eventData.Headers));

        var aggregateEvent = domainEvent.GetAggregateEvent();
        if (aggregateEvent is IHasRequestInfo hasRequestInfo)
        {
            var totalMilliseconds =
                DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - hasRequestInfo.RequestInfo.Date;

            if (totalMilliseconds > maxMillSeconds)
            {
                logger.LogInformation(
                    "Process domain event '{DomainEvent}' is too slow, time: {Timespan}ms, reqMsgId: {ReqMsgId}",
                    domainEvent.GetAggregateEvent().GetType().Name,
                    totalMilliseconds,
                    hasRequestInfo.RequestInfo.ReqMsgId);
            }
        }

        switch (aggregateEvent)
        {
            case ChannelCreatedEvent channelCreatedEvent:
                chatEventCacheHelper.Add(channelCreatedEvent);
                break;
            //case StartInviteToChannelEvent startInviteToChannelEvent:
            //    chatEventCacheHelper.Add(startInviteToChannelEvent);
            //    break;
        }

        await cachedReadModelUpdater.UpdateAsync([domainEvent], default);
        await dispatchToEventSubscribers.DispatchToSynchronousSubscribersAsync([domainEvent], default);
        sw.Stop();

        if (sw.Elapsed.TotalMilliseconds > maxMillSeconds)
        {
            logger.LogInformation("Process domain event '{DomainEvent}' is too slow, time {Timespan}ms",
                domainEvent.GetAggregateEvent().GetType().Name,
                sw.Elapsed);
        }
    }
}