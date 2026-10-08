using EventFlow.Configuration;
using EventFlow.Configuration.Cancellation;
using EventFlow.Core;
using EventFlow.Jobs;
using EventFlow.Subscribers;

namespace MyTelegram.Services.Services;

public class MyDomainEventPublisher(
    IDispatchToEventSubscribers dispatchToEventSubscribers,
    IJobScheduler jobScheduler,
    IServiceProvider serviceProvider,
    IEventFlowConfiguration eventFlowConfiguration,
    IEnumerable<ISubscribeSynchronousToAll> subscribeSynchronousToAlls,
    ICancellationConfiguration cancellationConfiguration,
    IShardedMessageQueueProcessor<WrappedReadModelDomainEvent> readModelShardedMessageQueueProcessor,
    IShardedMessageQueueProcessor<WrappedSagaDomainEvent> sagaShardedMessageQueueProcessor) : IDomainEventPublisher
{
    private readonly IReadOnlyCollection<ISubscribeSynchronousToAll> _subscribeSynchronousToAlls = subscribeSynchronousToAlls.ToList();

    public Task PublishAsync<TAggregate, TIdentity>(
        TIdentity id,
        IReadOnlyCollection<IDomainEvent> domainEvents,
        CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity
    {
        return PublishAsync(
            domainEvents,
            cancellationToken);
    }

    public async Task PublishAsync(
        IReadOnlyCollection<IDomainEvent> domainEvents,
        CancellationToken cancellationToken)
    {
        cancellationToken = cancellationConfiguration.Limit(cancellationToken, CancellationBoundary.BeforeUpdatingReadStores);
        foreach (var domainEvent in domainEvents)
        {
            readModelShardedMessageQueueProcessor.Enqueue(new WrappedReadModelDomainEvent(domainEvent), domainEvent.GetIdentity().Value);
        }

        cancellationToken = cancellationConfiguration.Limit(cancellationToken, CancellationBoundary.BeforeNotifyingSubscribers);
        await PublishToSubscribersOfAllEventsAsync(domainEvents, cancellationToken).ConfigureAwait(false);

        // Update subscriptions AFTER read stores have been updated
        await PublishToSynchronousSubscribersAsync(domainEvents, cancellationToken).ConfigureAwait(false);

        await PublishToAsynchronousSubscribersAsync(domainEvents, cancellationToken).ConfigureAwait(false);

        foreach (var domainEvent in domainEvents)
        {
            sagaShardedMessageQueueProcessor.Enqueue(new WrappedSagaDomainEvent(domainEvent), domainEvent.GetIdentity().Value);
        }
    }

    private async Task PublishToSubscribersOfAllEventsAsync(
        IReadOnlyCollection<IDomainEvent> domainEvents,
        CancellationToken cancellationToken)
    {
        var handle = _subscribeSynchronousToAlls
            .Select(s => s.HandleAsync(domainEvents, cancellationToken));
        await Task.WhenAll(handle).ConfigureAwait(false);
    }

    private async Task PublishToSynchronousSubscribersAsync(
        IReadOnlyCollection<IDomainEvent> domainEvents,
        CancellationToken cancellationToken)
    {
        await dispatchToEventSubscribers.DispatchToSynchronousSubscribersAsync(domainEvents, cancellationToken).ConfigureAwait(false);
    }

    private async Task PublishToAsynchronousSubscribersAsync(
        IEnumerable<IDomainEvent> domainEvents,
        CancellationToken cancellationToken)
    {
        if (eventFlowConfiguration.IsAsynchronousSubscribersEnabled)
        {
            await Task.WhenAll(domainEvents.Select(
                    d => jobScheduler.ScheduleNowAsync(
                        DispatchToAsynchronousEventSubscribersJob.Create(d, serviceProvider), cancellationToken)))
                .ConfigureAwait(false);
        }
    }
}