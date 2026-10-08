using EventFlow.Configuration;
using EventFlow.Core.Caching;
using EventFlow.Subscribers;
using Microsoft.Extensions.Caching.Memory;

namespace MyTelegram.EventFlow;

public sealed class MyDispatchToEventSubscribers(
    ILogger<DispatchToEventSubscribers> logger,
    IServiceProvider serviceProvider,
    IEventFlowConfiguration eventFlowConfiguration,
    IMemoryCache memoryCache,
    IDispatchToSubscriberResilienceStrategy dispatchToSubscriberResilienceStrategy)
    : IDispatchToEventSubscribers
{
    private static readonly Type SubscribeSynchronousToType = typeof(ISubscribeSynchronousTo<,,>);
    private static readonly Type SubscribeAsynchronousToType = typeof(ISubscribeAsynchronousTo<,,>);

    private sealed class SubscriberInformation
    {
        public object[] Subscribers = default!;
        public Func<object, IDomainEvent, CancellationToken, Task> HandleMethod = default!;
    }

    public async Task DispatchToSynchronousSubscribersAsync(
        IReadOnlyCollection<IDomainEvent> domainEvents,
        CancellationToken cancellationToken)
    {
        foreach (var domainEvent in domainEvents)
        {
            await dispatchToSubscriberResilienceStrategy.BeforeDispatchToSubscribersAsync(
                domainEvent,
                domainEvents,
                cancellationToken).ConfigureAwait(false);

            try
            {
                await DispatchToSubscribersAsync(
                        domainEvent,
                        SubscribeSynchronousToType,
                        !eventFlowConfiguration.ThrowSubscriberExceptions,
                        cancellationToken)
                    .ConfigureAwait(false);

                await dispatchToSubscriberResilienceStrategy.DispatchToSubscribersSucceededAsync(
                        domainEvent,
                        domainEvents,
                        cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await dispatchToSubscriberResilienceStrategy.HandleDispatchToSubscribersFailedAsync(
                        domainEvent,
                        domainEvents,
                        e,
                        cancellationToken)
                    .ConfigureAwait(false))
                {
                    throw;
                }
            }
        }
    }

    public Task DispatchToAsynchronousSubscribersAsync(
        IDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        return DispatchToSubscribersAsync(
            domainEvent,
            SubscribeAsynchronousToType,
            true,
            cancellationToken);
    }

    private async Task DispatchToSubscribersAsync(
        IDomainEvent domainEvent,
        Type subscriberType,
        bool swallowException,
        CancellationToken cancellationToken)
    {
        var subscriberInformation = GetSubscriberInformation(
            domainEvent.GetType(),
            subscriberType);

        var subscribers = subscriberInformation.Subscribers;

        if (subscribers.Length == 0)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("Didn't find any subscribers to {EventType}",
                    domainEvent.EventType.PrettyPrint());
            }
            return;
        }

        List<Exception>? exceptions = null;

        foreach (var subscriber in subscribers)
        {
            try
            {
                await DispatchToSubscriberAsync(
                        domainEvent,
                        (ISubscribe)subscriber,
                        subscriberInformation,
                        swallowException,
                        cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                exceptions ??= [];
                exceptions.Add(e);
            }
        }

        if (exceptions != null)
        {
            throw new AggregateException(
                $"Dispatch of domain event {domainEvent.GetType().PrettyPrint()} failed",
                exceptions);
        }
    }

    private async Task DispatchToSubscriberAsync(
        IDomainEvent domainEvent,
        ISubscribe subscriber,
        SubscriberInformation subscriberInformation,
        bool swallowException,
        CancellationToken cancellationToken)
    {
        await dispatchToSubscriberResilienceStrategy.BeforeHandleEventAsync(
                subscriber,
                domainEvent,
                cancellationToken)
            .ConfigureAwait(false);

        try
        {
            await subscriberInformation.HandleMethod(
                    subscriber,
                    domainEvent,
                    cancellationToken)
                .ConfigureAwait(false);

            await dispatchToSubscriberResilienceStrategy.HandleEventSucceededAsync(
                    subscriber,
                    domainEvent,
                    cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception e) when (swallowException)
        {
            await dispatchToSubscriberResilienceStrategy.HandleEventFailedAsync(
                    subscriber,
                    domainEvent,
                    e,
                    true,
                    cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            await dispatchToSubscriberResilienceStrategy.HandleEventFailedAsync(
                    subscriber,
                    domainEvent,
                    e,
                    false,
                    cancellationToken)
                .ConfigureAwait(false);
            throw;
        }
    }

    private SubscriberInformation GetSubscriberInformation(
        Type domainEventType,
        Type subscriberType)
    {
        return memoryCache.GetOrCreate(
            CacheKey.With(GetType(), domainEventType.GetCacheKey(), subscriberType.GetCacheKey()),
            _ =>
            {
                //entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12);

                var arguments = domainEventType
                    .GetInterfaces()
                    .First(i => i.IsGenericType &&
                                i.GetGenericTypeDefinition() == typeof(IDomainEvent<,,>))
                    .GetGenericArguments();
                var handlerType = subscriberType.MakeGenericType(arguments[0], arguments[1], arguments[2]);
                var services = serviceProvider.GetServices(handlerType);

                var list = new List<object>();
                foreach (var s in services)
                {
                    if (s != null)
                    {
                        list.Add(s);
                    }
                }

                var invokeHandleAsync = ReflectionHelper.CompileMethodInvocation<Func<object, IDomainEvent, CancellationToken, Task>>(handlerType, "HandleAsync");

                return new SubscriberInformation
                {
                    Subscribers = list.ToArray(),
                    HandleMethod = invokeHandleAsync
                };
            })!;
    }
}