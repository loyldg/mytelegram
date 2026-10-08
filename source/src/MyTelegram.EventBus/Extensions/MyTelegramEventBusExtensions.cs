using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace MyTelegram.EventBus.Extensions;
public static class MyTelegramEventBusExtensions
{
    public static IServiceCollection AddSubscription<T, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TH>(this IServiceCollection services, bool useRawDataHandler = false)
    where T : class
        where TH : class, IEventHandler<T>
    {
        // Use keyed services to register multiple handlers for the same event type
        // the consumer can use IKeyedServiceProvider.GetKeyedService<IIntegrationEventHandler>(typeof(T)) to get all
        // handlers for the event type.
        services.AddKeyedTransient<IEventHandler<T>, TH>(typeof(T));
        services.Configure<EventBusSubscriptionInfo>(o =>
        {
            // Keep track of all registered event types and their name mapping. We send these event types over the message bus
            // and we don't want to do Type.GetType, so we keep track of the name mapping here.

            // This list will also be used to subscribe to events from the underlying message broker implementation.
            o.EventTypes[typeof(T).Name] = typeof(T);
            if (useRawDataHandler)
            {
                o.RawDataHandledEventTypes.Add(typeof(T).Name);
            }
            o.Register<T>();

        });
        //services.AddTransient<IEventBus, NullEventBus>();

        return services;
    }
}
