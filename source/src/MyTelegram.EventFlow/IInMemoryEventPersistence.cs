namespace MyTelegram.EventFlow;

public interface IInMemoryEventPersistence : IEventPersistence
{
    //Task<IReadOnlyCollection<IDomainEvent<TAggregate, TIdentity>>> CommitEventsAsync<TAggregate, TIdentity>(
    //    TIdentity id,
    //    IReadOnlyCollection<IUncommittedEvent> events,
    //    CancellationToken cancellationToken)
    //    where TAggregate : IAggregateRoot<TIdentity>
    //    where TIdentity : IIdentity;

    IReadOnlyCollection<IDomainEvent<TAggregate, TIdentity>> CommitEvents<TAggregate, TIdentity>(
        TIdentity id,
        IReadOnlyCollection<IUncommittedEvent> events)
        where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity;

    Task<IReadOnlyCollection<IDomainEvent<TAggregate, TIdentity>>> LoadEventsAsync<TAggregate, TIdentity>(
        TIdentity id,
        int fromEventSequenceNumber,
        CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity;

    Task<IReadOnlyCollection<IDomainEvent<TAggregate, TIdentity>>> LoadEventsAsync<TAggregate, TIdentity>(
        TIdentity id,
        int fromEventSequenceNumber,
        int toEventSequenceNumber,
        CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity;
}