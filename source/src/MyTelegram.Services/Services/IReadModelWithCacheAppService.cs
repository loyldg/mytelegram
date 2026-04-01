using EventFlow.ReadStores;

namespace MyTelegram.Services.Services;

public interface IReadModelWithCacheAppService<TReadModel> : IReadModelWithCacheAppService<TReadModel, long>
    where TReadModel : IReadModel
{
}

public interface IReadModelWithCacheAppService<TReadModel, in TKey>
    where TReadModel : IReadModel
{
    Task<TReadModel> GetAsync(TKey id, bool throwIfNotExists = true);
    Task<IReadOnlyCollection<TReadModel>> GetListAsync(IEnumerable<TKey> ids);
}