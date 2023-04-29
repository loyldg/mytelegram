namespace MyTelegram.EventBus;

// taken from https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/BuildingBlocks/EventBus/EventBus/IEventBusSubscriptionsManager.cs
public interface IEventBusSubscriptionsManager
{
    bool IsEmpty { get; }

    void AddSubscription<T, TH>()
        where TH : IEventHandler<T>;

    void Clear();
    string GetEventKey<T>();
    string GetEventKey(Type type);
    Type? GetEventTypeByName(string eventName);
    IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>();
    IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

    bool HasSubscriptionsForEvent<T>();
    bool HasSubscriptionsForEvent(string eventName);
    event EventHandler<string> OnEventRemoved;

    void RemoveSubscription<T, TH>()
        where TH : IEventHandler<T>;
}
