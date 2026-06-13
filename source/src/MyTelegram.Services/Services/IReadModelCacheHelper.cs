namespace MyTelegram.Services.Services;

public interface IReadModelCacheHelper<TReadModel> : IReadModelCacheHelper<TReadModel, long>
{

}

public interface IReadModelCacheHelper<TReadModel, in TKey>
{
    Task<TReadModel?> GetOrCreateAsync(TKey id, Func<Task<TReadModel?>> createFactory, Func<TReadModel, string> createReadModelIdFunc);
    bool TryGetReadModel(TKey readModelId, out TReadModel? readModel);
    bool TryGetReadModelById(string readModelId, out TReadModel? readModel);
    void Add(TKey id, string readModelId, TReadModel readModel);
    void Remove(string readModelId);
}