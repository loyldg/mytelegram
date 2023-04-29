using System.Collections.Concurrent;

namespace MyTelegram.EventBus;

public class EventHandlerInvoker : IEventHandlerInvoker
{
    private readonly ConcurrentDictionary<string, IEventHandlerMethodExecutor> _cache;

    public EventHandlerInvoker()
    {
        _cache = new ConcurrentDictionary<string, IEventHandlerMethodExecutor>();
    }

    public async Task InvokeAsync(IEventHandler eventHandler,
        object eventData,
        Type eventType)
    {
        var cacheItem = _cache.GetOrAdd($"{eventHandler.GetType().FullName}-{eventType.FullName}",
            _ =>
            {
                var type = typeof(DistributedEventHandlerMethodExecutor<>).MakeGenericType(eventType);
                var obj = Activator.CreateInstance(type);
                if (obj == null)
                {
                    throw new InvalidOperationException($"Failed to create instance for type:{type}");
                }

                return (IEventHandlerMethodExecutor)obj;
            });

        if (cacheItem == null)
        {
            throw new Exception("The object instance is not an event handler. Object type: " +
                                eventHandler.GetType().AssemblyQualifiedName);
        }

        await cacheItem.ExecutorAsync(eventHandler, eventData);
    }
}
