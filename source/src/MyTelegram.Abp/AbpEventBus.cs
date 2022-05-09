namespace MyTelegram.Abp;

public class AbpEventBus : IEventBus
{
    private readonly IDistributedEventBus _distributedEventBus;

    public AbpEventBus(IDistributedEventBus distributedEventBus)
    {
        _distributedEventBus = distributedEventBus;
    }

    public Task PublishAsync<TEventData>(TEventData eventData)
    {
        return _distributedEventBus.PublishAsync(eventData?.GetType(), eventData);
    }

    public Task PublishAsync(Type eventDataType,
        object eventData)
    {
        return _distributedEventBus.PublishAsync(eventDataType, eventData);
    }
}