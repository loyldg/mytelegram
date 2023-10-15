using EventFlow.ReadStores;

namespace MyTelegram.Messenger.QueryServer.EventHandlers;
public class DistributedDomainEventHandler : IEventHandler<DomainEventMessage>
//,
//IDistributedEventHandler<DomainEventMessage>, ITransientDependency
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

    public async Task HandleEventAsync(DomainEventMessage eventData)
    {
        //var sw = Stopwatch.StartNew();



        //Task.Run(() =>
        //{
        //    _dispatchToReadStores.DispatchAsync(new[] { domainEvent }, default);
        //});



        //await _dispatchToReadStores.DispatchAsync(new[] { domainEvent }, default);

        Task.Run(async () =>
        {
            var sw = Stopwatch.StartNew();
            var domainEvent = _eventJsonSerializer.Deserialize(eventData.Message, new Metadata(eventData.Headers));
            //Console.WriteLine($"domain event:{domainEvent.GetAggregateEvent().GetType().Name}");
            if (domainEvent.GetAggregateEvent() is IHasRequestInfo hasRequestInfo)
            {
                var ts = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - hasRequestInfo.RequestInfo.Date;

                if (ts > 500)
                {
                    _logger.LogWarning("DomainEvent:{EventName},timespan:{TimeSpan} {Id} ",
                        domainEvent.GetAggregateEvent().GetType().Name, ts, hasRequestInfo.RequestInfo.ReqMsgId);
                }
            }

            await _dispatchToReadStores.DispatchAsync(new[] { domainEvent }, default);
            await _dispatchToEventSubscribers.DispatchToSynchronousSubscribersAsync(new[] { domainEvent }, default);
            sw.Stop();

            //if (sw.Elapsed.TotalMilliseconds > 50)
            //{
            //    Console.WriteLine($"##### [{sw.Elapsed}]domain event:{domainEvent.GetAggregateEvent().GetType().Name}");
            //}
        });


        //_dispatchToReadStores.DispatchAsync(new[] { domainEvent }, default);
        // _dispatchToEventSubscribers.DispatchToSynchronousSubscribersAsync(new[] { domainEvent }, default);
        //Console.WriteLine($"Receive domain event:{eventData.EventId} {domainEvent.GetAggregateEvent().GetType().Name}");

        //await _dispatchToEventSubscribers.DispatchToAsynchronousSubscribersAsync(domainEvent, default);
        //sw.Stop();

        //if (sw.Elapsed.TotalMilliseconds > 100)
        //{
        //    _logger.LogWarning("DomainEvent:{EventName},timespan:{TimeSpan}  {Total}",
        //        domainEvent.GetAggregateEvent().GetType().Name, sw.Elapsed,sw.Elapsed.TotalMilliseconds);
        //}


    }
}
