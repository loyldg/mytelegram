using System.Collections.Concurrent;

namespace MyTelegram.Services.Services;

public class ReadModelCacheHelper<TReadModel, TKey> : IReadModelCacheHelper<TReadModel, TKey> where TKey : notnull
{
    private static readonly ConcurrentDictionary<TKey, TReadModel> ReadModels = [];
    private static readonly ConcurrentDictionary<string, TKey> ReadModelIds = [];
    public async Task<TReadModel?> GetOrCreateAsync(TKey id, Func<Task<TReadModel?>> createFactory, Func<TReadModel, string> createReadModelIdFunc)
    {
        if (ReadModels.TryGetValue(id, out var readModel))
        {
            return readModel;
        }

        readModel = await createFactory();
        if (readModel == null)
        {
            return readModel;
        }

        ReadModels.TryAdd(id, readModel!);

        var readModelId = createReadModelIdFunc(readModel!);
        ReadModelIds.TryAdd(readModelId, id);

        return readModel;
    }

    public bool TryGetReadModel(TKey readModelId, out TReadModel? readModel)
    {
        return ReadModels.TryGetValue(readModelId, out readModel);
    }

    public bool TryGetReadModelById(string readModelId, out TReadModel? readModel)
    {
        if (ReadModelIds.TryGetValue(readModelId, out var id))
        {
            return ReadModels.TryGetValue(id, out readModel);
        }

        readModel = default;

        return false;
    }

    public void Add(TKey id, string readModelId, TReadModel readModel)
    {
        ReadModelIds.TryAdd(readModelId, id);
        ReadModels.TryAdd(id, readModel);
    }

    public void Remove(string readModelId)
    {
        if (ReadModelIds.TryRemove(readModelId, out var id))
        {
            ReadModels.TryRemove(id, out _);
        }
    }

    public TReadModel? Get(string readModelId)
    {
        if (ReadModelIds.TryGetValue(readModelId, out var id))
        {
            ReadModels.TryGetValue(id, out var readModel);
            return readModel;
        }

        return default;
    }
}


public class ReadModelCacheHelper<TReadModel> : ReadModelCacheHelper<TReadModel, long>, IReadModelCacheHelper<TReadModel>
{

}