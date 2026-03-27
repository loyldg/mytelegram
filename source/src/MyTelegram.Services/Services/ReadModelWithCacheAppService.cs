using EventFlow.ReadStores;

namespace MyTelegram.Services.Services;

public abstract class ReadModelWithCacheAppService<TReadModel>(IReadModelCacheHelper<TReadModel> cacheHelper) : IReadModelWithCacheAppService<TReadModel>
    where TReadModel : IReadModel
{
    public Task<TReadModel?> GetAsync(long? id)
    {
        TReadModel? readModel = default;
        if (!id.HasValue || id == 0)
        {
            return Task.FromResult(readModel);
        }

        return cacheHelper.GetOrCreateAsync(id.Value, () => GetReadModelAsync(id.Value), GetReadModelId);
    }

    public async Task<TReadModel> GetAsync(long id)
    {
        var readModel = await cacheHelper.GetOrCreateAsync(id, () => GetReadModelAsync(id), GetReadModelId);

        return readModel ?? throw new ArgumentException($"ReadModel({typeof(TReadModel).Name}) with id {id} not exists");
    }

    protected virtual bool IsValidReadModel(TReadModel readModel)
    {
        return true;
    }

    public async Task<IReadOnlyCollection<TReadModel>> GetListAsync(IEnumerable<long> ids)
    {
        //var readModels = new List<TReadModel>();
        var readModels = new Dictionary<long, TReadModel>();
        var idsNotExistInCache = new HashSet<long>();
        foreach (var id in ids)
        {
            if (cacheHelper.TryGetReadModel(id, out var readModel))
            {
                if (readModel != null)
                {
                    if (IsValidReadModel(readModel))
                    {
                        readModels.TryAdd(id, readModel);
                    }
                }
                else
                {
                    idsNotExistInCache.Add(id);
                }
            }
            else
            {
                idsNotExistInCache.Add(id);
            }
        }

        if (idsNotExistInCache.Count > 0)
        {
            var dbReadModels = await GetReadModelListAsync([.. idsNotExistInCache]);
            //readModels.AddRange(dbReadModels);

            foreach (var readModel in dbReadModels)
            {
                if (!IsValidReadModel(readModel))
                {
                    continue;
                }
                readModels.TryAdd(GetReadModelInt64Id(readModel), readModel);
                cacheHelper.Add(GetReadModelInt64Id(readModel), GetReadModelId(readModel), readModel);
                idsNotExistInCache.Remove(GetReadModelInt64Id(readModel));
            }

            foreach (var id in idsNotExistInCache)
            {
                var readModel = await CreateNonExistsReadModelAsync(id);
                if (readModel != null)
                {
                    if (!IsValidReadModel(readModel))
                    {
                        continue;
                    }

                    readModels.TryAdd(id, readModel);
                    //cacheHelper.Add(id, GetReadModelId(readModel), readModel);
                }
            }
        }

        return readModels.Values;
    }

    protected abstract Task<TReadModel?> CreateNonExistsReadModelAsync(long id);

    protected abstract Task<IReadOnlyCollection<TReadModel>> GetReadModelListAsync(List<long> ids);

    protected abstract Task<TReadModel?> GetReadModelAsync(long id);
    protected abstract string GetReadModelId(TReadModel readModel);
    protected abstract long GetReadModelInt64Id(TReadModel readModel);
}