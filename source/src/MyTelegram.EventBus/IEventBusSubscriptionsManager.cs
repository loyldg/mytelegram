namespace MyTelegram.EventBus;

// taken from https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/BuildingBlocks/EventBus/EventBus/IEventBusSubscriptionsManager.cs
public interface IEventBusSubscriptionsManager
{
    bool IsEmpty { get; }
    event EventHandler<string> OnEventRemoved;
    void AddSubscription<T, TH>()
        where TH : IEventHandler<T>;

    void RemoveSubscription<T, TH>()
        where TH : IEventHandler<T>;

    bool HasSubscriptionsForEvent<T>();
    bool HasSubscriptionsForEvent(string eventName);
    Type? GetEventTypeByName(string eventName);
    void Clear();
    IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>();
    IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
    string GetEventKey<T>();
    string GetEventKey(Type type);
}