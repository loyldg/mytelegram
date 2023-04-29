namespace MyTelegram.Domain.EventFlow;

public abstract class MyInMemoryAggregateSaga<TSaga, TIdentity, TLocator> : AggregateSaga<TSaga, TIdentity, TLocator>
    , IInMemoryAggregate, ISagaErrorHandler<TSaga>
    where TSaga : AggregateSaga<TSaga, TIdentity, TLocator>
    where TIdentity : ISagaId
    where TLocator : ISagaLocator
{
    private readonly IEventStore _eventStore;

    protected MyInMemoryAggregateSaga(TIdentity id,
        IEventStore eventStore) : base(id)
    {
        _eventStore = eventStore;
    }

    public async Task<bool> HandleAsync(ISagaId sagaId,
        SagaDetails sagaDetails,
        Exception exception,
        CancellationToken cancellationToken)
    {
        await CompleteAsync(cancellationToken);
        return false;
    }

    /// <summary>
    ///     Mark the saga as completed and remove this saga from memory
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        Complete();
        // Aggregate emit events will not be completed immediately,so delay 100ms to remove it from memory
        Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken).ContinueWith(_ =>
                _eventStore.DeleteAggregateAsync<TSaga, TIdentity>(Id, cancellationToken),
            cancellationToken);
        return Task.CompletedTask;
    }
}
