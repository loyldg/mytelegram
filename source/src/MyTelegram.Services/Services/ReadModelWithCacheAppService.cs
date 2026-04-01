using EventFlow.ReadStores;

namespace MyTelegram.Services.Services;

public abstract class ReadModelWithCacheAppService<TReadModel>(IReadModelCacheHelper<TReadModel, long> cacheHelper)
    : ReadModelWithCacheAppService<TReadModel, long>(cacheHelper) where TReadModel : IReadModel;

public abstract class ReadModelWithCacheAppService<TReadModel, TKey>(IReadModelCacheHelper<TReadModel, TKey> cacheHelper) : IReadModelWithCacheAppService<TReadModel, TKey>
    where TReadModel : IReadModel where TKey : notnull
{
    public virtual async Task<TReadModel> GetAsync(TKey id, bool throwIfNotExists = true)
    {
        var readModel = await cacheHelper.GetOrCreateAsync(id, () => GetReadModelAsync(id), GetReadModelId);
        if (readModel == null)
        {
            readModel = await CreateNonExistsReadModelAsync(id);
        }
        if (readModel == null && throwIfNotExists)
        {
            throw new ArgumentException($"ReadModel({typeof(TReadModel).Name}) with id {id} not exists");
        }

        return readModel!;
    }

    protected virtual bool IsValidReadModel(TReadModel readModel)
    {
        return true;
    }

    public async Task<IReadOnlyCollection<TReadModel>> GetListAsync(IEnumerable<TKey> ids)
    {
        //var readModels = new List<TReadModel>();
        var readModels = new Dictionary<TKey, TReadModel>();
        var idsNotExistInCache = new HashSet<TKey>();
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
                readModels.TryAdd(GetReadModelKey(readModel), readModel);
                cacheHelper.Add(GetReadModelKey(readModel), GetReadModelId(readModel), readModel);
                idsNotExistInCache.Remove(GetReadModelKey(readModel));
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

    protected abstract Task<TReadModel?> CreateNonExistsReadModelAsync(TKey id);

    protected abstract Task<IReadOnlyCollection<TReadModel>> GetReadModelListAsync(List<TKey> ids);

    protected abstract Task<TReadModel?> GetReadModelAsync(TKey id);
    protected abstract string GetReadModelId(TReadModel readModel);
    protected abstract TKey GetReadModelKey(TReadModel readModel);

}