using System.Collections.Concurrent;
using System.Reflection;

namespace MyTelegram.EventBus;

// taken from https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/BuildingBlocks/EventBus/EventBus/InMemoryEventBusSubscriptionsManager.cs
public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
{
    private readonly Dictionary<string, List<SubscriptionInfo>> _handlers = new();
    private readonly ConcurrentDictionary<string, Type> _eventNameToEventTypes = new();
    public event EventHandler<string>? OnEventRemoved;

    public bool IsEmpty => _handlers is { Count: 0 };
    public void Clear() => _handlers.Clear();


    public void AddSubscription<T, TH>()
        where TH : IEventHandler<T>
    {
        var eventName = GetEventKey<T>();

        DoAddSubscription(typeof(TH), eventName);

        if (!_eventNameToEventTypes.ContainsKey(eventName))
        {
            _eventNameToEventTypes.TryAdd(eventName, typeof(T));
        }
    }

    private void DoAddSubscription(Type handlerType, string eventName)
    {
        if (!HasSubscriptionsForEvent(eventName))
        {
            _handlers.Add(eventName, new List<SubscriptionInfo>());
        }

        if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
        {
            throw new ArgumentException(
                $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
        }

        _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
    }

    public void RemoveSubscription<T, TH>()
        where TH : IEventHandler<T>
    {
        var handlerToRemove = FindSubscriptionToRemove<T, TH>();
        var eventName = GetEventKey<T>();
        DoRemoveHandler(eventName, handlerToRemove);
    }


    private void DoRemoveHandler(string eventName, SubscriptionInfo? subsToRemove)
    {
        if (subsToRemove != null)
        {
            _handlers[eventName].Remove(subsToRemove);
            if (!_handlers[eventName].Any())
            {
                _handlers.Remove(eventName);
                _eventNameToEventTypes.TryRemove(eventName, out _);

                RaiseOnEventRemoved(eventName);
            }
        }
    }

    public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>()
    {
        var key = GetEventKey<T>();
        return GetHandlersForEvent(key);
    }
    public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => _handlers[eventName];

    private void RaiseOnEventRemoved(string eventName)
    {
        var handler = OnEventRemoved;
        handler?.Invoke(this, eventName);
    }


    private SubscriptionInfo? FindSubscriptionToRemove<T, TH>()
        where TH : IEventHandler<T>
    {
        var eventName = GetEventKey<T>();
        return DoFindSubscriptionToRemove(eventName, typeof(TH));
    }

    private SubscriptionInfo? DoFindSubscriptionToRemove(string eventName, Type handlerType)
    {
        if (!HasSubscriptionsForEvent(eventName))
        {
            return null;
        }

        return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);
    }

    public bool HasSubscriptionsForEvent<T>()
    {
        var key = GetEventKey<T>();
        return HasSubscriptionsForEvent(key);
    }
    public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

    public Type? GetEventTypeByName(string eventName)
    {
        _eventNameToEventTypes.TryGetValue(eventName, out var eventType);

        return eventType;
    }

    public string GetEventKey(Type type)
    {
        var attr = type.GetCustomAttribute<EventNameAttribute>();
        if (attr != null)
        {
            return attr.Name;
        }

        return $"{type.Namespace}.{type.Name}";
    }

    public string GetEventKey<T>()
    {
        var type = typeof(T);
        return GetEventKey(type);
    }
}