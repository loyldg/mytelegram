using EventFlow.ReadStores;

namespace MyTelegram.Messenger.QueryServer.EventHandlers;
public class DistributedDomainEventHandler : IEventHandler<DomainEventMessage>
{
    private readonly IEventJsonSerializer _eventJsonSerializer;
    private readonly IDispatchToEventSubscribers _dispatchToEventSubscribers;
    private readonly ILogger<DistributedDomainEventHandler> _logger;
    private readonly IDispatchToReadStores _dispatchToReadStores;
    public DistributedDomainEventHandler(IEventJsonSerializer eventJsonSerializer, IDispatchToEventSubscribers dispatchToEventSubscribers, ILogger<DistributedDomainEventHandler> logger, IDispatchToReadStores dispatchToReadStores)
    {
        _eventJsonSerializer = eventJsonSerializer;
        _dispatchToEventSubscribers = dispatchToEventSubscribers;
        _logger = logger;
        _dispatchToReadStores = dispatchToReadStores;
    }

    public Task HandleEventAsync(DomainEventMessage eventData)
    {
        Task.Run(async () =>
        {
            var maxMillSeconds = 500;
            var sw = Stopwatch.StartNew();
            var domainEvent = _eventJsonSerializer.Deserialize(eventData.Message, new Metadata(eventData.Headers));
            if (domainEvent.GetAggregateEvent() is IHasRequestInfo hasRequestInfo)
            {
                var totalMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - hasRequestInfo.RequestInfo.Date;

                if (totalMilliseconds > maxMillSeconds)
                {
                    _logger.LogWarning("Process domain event '{DomainEvent}' is too slow,time={Timespan}ms,reqMsgId={ReqMsgId}",
                        domainEvent.GetAggregateEvent().GetType().Name,
                        totalMilliseconds,
                        hasRequestInfo.RequestInfo.ReqMsgId);
                }
            }

            await _dispatchToReadStores.DispatchAsync(new[] { domainEvent }, default);
            await _dispatchToEventSubscribers.DispatchToSynchronousSubscribersAsync(new[] { domainEvent }, default);
            sw.Stop();

            if (sw.Elapsed.TotalMilliseconds > maxMillSeconds)
            {
                _logger.LogWarning("Process domain event '{DomainEvent}' is too slow,time={Timespan}ms",
                    domainEvent.GetAggregateEvent().GetType().Name,
                    sw.Elapsed);
            }
        });

        return Task.CompletedTask;
    }
}
