using EventFlow.ReadStores;
using EventFlow.Sagas;

namespace MyTelegram.Services.Services;

public record WrappedReadModelDomainEvent(IDomainEvent DomainEvent);
public record WrappedSagaDomainEvent(IDomainEvent DomainEvent);

public class WrappedReadModelDomainEventDataProcessor(IDispatchToReadStores dispatchToReadStores) : IDataProcessor<WrappedReadModelDomainEvent>, ITransientDependency
{
    public Task ProcessAsync(WrappedReadModelDomainEvent data, CancellationToken cancellationToken = default)
    {
        return dispatchToReadStores.DispatchAsync([data.DomainEvent], cancellationToken);
    }
}

public class WrappedSagaDomainEventDataProcessor(IDispatchToSagas dispatchToSagas) : IDataProcessor<WrappedSagaDomainEvent>, ITransientDependency
{
    public Task ProcessAsync(WrappedSagaDomainEvent data, CancellationToken cancellationToken = default)
    {
        return dispatchToSagas.ProcessAsync([data.DomainEvent], cancellationToken);
    }
}