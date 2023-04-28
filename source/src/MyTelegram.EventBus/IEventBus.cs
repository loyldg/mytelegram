namespace MyTelegram.EventBus;

public interface IEventBus
{
    Task PublishAsync<TEventData>(TEventData eventData) where TEventData : class;

    Task PublishAsync(Type eventDataType,
        object eventData);

    void Subscribe<TEvent, TEventHandler>() where TEventHandler : IEventHandler<TEvent>;
    void Unsubscribe<TEvent, TEventHandler>() where TEventHandler : IEventHandler<TEvent>;
}