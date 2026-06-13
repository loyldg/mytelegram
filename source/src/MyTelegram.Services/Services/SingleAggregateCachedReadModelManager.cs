using EventFlow.ReadStores;

namespace MyTelegram.Services.Services;

public class SingleAggregateCachedReadModelManager<TReadModelInterface, TReadModel>(
    IReadModelDomainEventApplier readModelDomainEventApplier,
    IServiceProvider serviceProvider,
    IReadModelCacheHelper<TReadModelInterface> readModelCacheHelper) :
    CachedReadModelManager<TReadModelInterface, TReadModel, long>(readModelDomainEventApplier, serviceProvider,
        readModelCacheHelper) where TReadModel : class, IReadModel where TReadModelInterface : IReadModel
{
    protected override IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        yield return domainEvent.GetIdentity().Value;
    }
}

public class SingleAggregateCachedReadModelManager<TReadModelInterface, TReadModel, TKey>(
    IReadModelDomainEventApplier readModelDomainEventApplier,
    IServiceProvider serviceProvider,
    IReadModelCacheHelper<TReadModelInterface, TKey> readModelCacheHelper) :
    CachedReadModelManager<TReadModelInterface, TReadModel, TKey>(readModelDomainEventApplier, serviceProvider,
        readModelCacheHelper) where TReadModel : class, IReadModel where TReadModelInterface : IReadModel
{
    protected override IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        yield return domainEvent.GetIdentity().Value;
    }
}