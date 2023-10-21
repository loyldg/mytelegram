using EventFlow.Aggregates;
using EventFlow.Core;
using EventFlow.Core.Caching;
using EventFlow.Core.RetryStrategies;
using EventFlow.Extensions;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace MyTelegram.EventFlow.MongoDB;

public class MyMongoDbReadModelStore<TReadModel> :
    MyMongoDbReadModelStore<TReadModel, IMongoDbContext>
    where TReadModel : class, IMongoDbReadModel
{
    public MyMongoDbReadModelStore(ILogger<MongoDbReadModelStore<TReadModel>> logger, IReadModelDescriptionProvider readModelDescriptionProvider, ITransientFaultHandler<IOptimisticConcurrencyRetryStrategy> transientFaultHandler, IMongoDbContextFactory<IMongoDbContext> dbContextFactory, IMemoryCache memoryCache, IReadModelCacheStrategy readModelCacheStrategy, IReadModelUpdateStrategy readModelUpdateStrategy, IReadModelUpdateManager readModelUpdateManager) : base(logger, readModelDescriptionProvider, transientFaultHandler, dbContextFactory, memoryCache, readModelCacheStrategy, readModelUpdateStrategy, readModelUpdateManager)
    {
    }
}

public class MyMongoDbReadModelStore<TReadModel, TDbContext> : MongoDbReadModelStore<TReadModel>, IMyMongoDbReadModelStore<TReadModel> where TReadModel : class, IMongoDbReadModel
where TDbContext : IMongoDbContext
{
    private readonly ILogger<MongoDbReadModelStore<TReadModel>> _logger;
    //private readonly IMongoDbReadModelDatabaseFactory _databaseFactory;
    private readonly IMongoDbContextFactory<TDbContext> _dbContextFactory;
    private readonly IReadModelDescriptionProvider _readModelDescriptionProvider;
    private readonly IMemoryCache _memoryCache;
    private readonly IReadModelCacheStrategy _readModelCacheStrategy;
    private readonly IReadModelUpdateStrategy _readModelUpdateStrategy;
    //private readonly IMyInMemoryReadStore<TReadModel> _myInMemoryReadStore;
    private readonly IReadModelUpdateManager _readModelUpdateManager;

    public MyMongoDbReadModelStore(ILogger<MongoDbReadModelStore<TReadModel>> logger,
        IReadModelDescriptionProvider readModelDescriptionProvider,
        ITransientFaultHandler<IOptimisticConcurrencyRetryStrategy> transientFaultHandler,
        IMongoDbContextFactory<TDbContext> dbContextFactory,
        IMemoryCache memoryCache,
        IReadModelCacheStrategy readModelCacheStrategy,
        IReadModelUpdateStrategy readModelUpdateStrategy, IReadModelUpdateManager readModelUpdateManager)
        : base(logger, dbContextFactory.CreateContext().GetDatabase(), readModelDescriptionProvider, transientFaultHandler)
    {
        _logger = logger;
        _readModelDescriptionProvider = readModelDescriptionProvider;
        _dbContextFactory = dbContextFactory;
        _memoryCache = memoryCache;
        _readModelCacheStrategy = readModelCacheStrategy;
        _readModelUpdateStrategy = readModelUpdateStrategy;
        _readModelUpdateManager = readModelUpdateManager;
        //_myInMemoryReadStore = myInMemoryReadStore;
    }

    private IMongoDatabase GetDatabase() => _dbContextFactory.CreateContext().GetDatabase();

    public Task<IAggregateFluent<TResult>> AggregateAsync<TResult, TKey>(
        Expression<Func<TReadModel, bool>> filter,
        Expression<Func<TReadModel, TKey>> id,
        Expression<Func<IGrouping<TKey, TReadModel>, TResult>> group,
        AggregateOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = GetDatabase().GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);

        return Task.FromResult(collection.Aggregate()
                .Match(filter)
                .Group(id, group))
            ;
    }

    public async Task<IAsyncCursor<TResult>> FindAsync<TResult>(Expression<Func<TReadModel, bool>> filter, FindOptions<TReadModel, TResult>? options = null, CancellationToken cancellationToken = default)
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = GetDatabase().GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);

        _logger.LogTrace(
            "Finding read model '{ReadModel}' with expression '{Filter}' from collection '{RootCollectionName}'",
            typeof(TReadModel).PrettyPrint(),
            filter,
            readModelDescription.RootCollectionName);

        return await collection.FindAsync(filter, options, cancellationToken);
    }

    public Task<long> CountAsync(Expression<Func<TReadModel, bool>>? filter = null, CancellationToken cancellationToken = default)
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = GetDatabase().GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);

        return collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
    }

    public override async Task<ReadModelEnvelope<TReadModel>> GetAsync(string id, CancellationToken cancellationToken)
    {
        var updateStrategy = await _readModelUpdateManager.GetReadModelUpdateStrategyAsync<TReadModel>();
        //if (await _readModelCacheStrategy.ShouldCacheReadModelAsync<TReadModel>())
        if (updateStrategy == UpdateStrategy.All || updateStrategy == UpdateStrategy.UpdateCache)
        {
            var item = await _memoryCache.GetOrCreateAsync(CacheKey.With(typeof(TReadModel), id),
                cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromDays(3);
                    Console.WriteLine($"Get from db to cache:{id}");
                    return base.GetAsync(id, cancellationToken);
                });

            if (item == null)
            {
                return ReadModelEnvelope<TReadModel>.Empty(id);
            }

            return item;
        }
        //Console.WriteLine($"Get from db :{id}");
        return await base.GetAsync(id, cancellationToken);
    }

    public override async Task UpdateAsync(IReadOnlyCollection<ReadModelUpdate> readModelUpdates, IReadModelContextFactory readModelContextFactory,
        Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken, Task<ReadModelUpdateResult<TReadModel>>> updateReadModel, CancellationToken cancellationToken)
    {
        var updateStrategy = await _readModelUpdateManager.GetReadModelUpdateStrategyAsync<TReadModel>();

        //await base.UpdateAsync(readModelUpdates, readModelContextFactory, updateReadModel, cancellationToken);
        switch (updateStrategy)
        {
            case UpdateStrategy.None:
                return;
            case UpdateStrategy.All:

                Task<ReadModelUpdateResult<TReadModel>> UpdateReadModelWithCaching(IReadModelContext context, IReadOnlyCollection<IDomainEvent> events, ReadModelEnvelope<TReadModel> envelope, CancellationToken token) =>
                    updateReadModel(context, events, envelope, token)
                        .ContinueWith(result =>
                        {
                            if (!result.IsFaulted)
                            {
                                //_memoryCache.Set(CacheKey.With(typeof(TReadModel), result.Result.Envelope.ReadModelId), result.Result.Envelope);
                                UpdateInMemoryReadModelAsync(result.Result.Envelope);
                            }

                            return result.Result;
                        }, token);

                await base.UpdateAsync(readModelUpdates, readModelContextFactory, UpdateReadModelWithCaching, cancellationToken);
                //await UpdateReadModelInCacheAsync(readModelUpdates, readModelContextFactory, updateReadModel,
                //    cancellationToken);
                break;
            case UpdateStrategy.UpdateDatabase:
                await base.UpdateAsync(readModelUpdates, readModelContextFactory, updateReadModel, cancellationToken);
                break;
            case UpdateStrategy.UpdateCache:
                {
                    await UpdateReadModelInCacheAsync(readModelUpdates, readModelContextFactory, updateReadModel, cancellationToken);
                }
                break;
        }
        async Task UpdateReadModelInCacheAsync(IReadOnlyCollection<ReadModelUpdate> readOnlyCollection,
            IReadModelContextFactory readModelContextFactory1, Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken, Task<ReadModelUpdateResult<TReadModel>>> func, CancellationToken cancellationToken1)
        {
            foreach (var readModelUpdate in readOnlyCollection)
            {
                if (readModelUpdate.DomainEvents.Count == 0)
                {
                    continue;
                }

                var readModelId = readModelUpdate.ReadModelId;
                var readModelEnvelope = await GetAsync(readModelId, default);
                var readModelContext = readModelContextFactory1.Create(readModelId, readModelEnvelope.ReadModel == null);
                var readModelUpdateResult = await func(
                    readModelContext,
                    readModelUpdate.DomainEvents,
                    readModelEnvelope,
                    cancellationToken1);

                if (!readModelUpdateResult.IsModified)
                {
                    continue;
                }

                readModelEnvelope = readModelUpdateResult.Envelope;

                if (readModelContext.IsMarkedForDeletion)
                {
                    _memoryCache.Remove(CacheKey.With(typeof(TReadModel), readModelId));

                    Console.WriteLine($"Delete in memory readModel:{readModelId}");
                }
                else
                {
                    await UpdateInMemoryReadModelAsync(readModelEnvelope);
                }
            }
        }
    }

    private Task UpdateInMemoryReadModelAsync(ReadModelEnvelope<TReadModel> envelope)
    {
        _memoryCache.Set(CacheKey.With(typeof(TReadModel), envelope.ReadModelId), envelope);
        return Task.CompletedTask;
    }
}