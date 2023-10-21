using EventFlow.Aggregates;
using EventFlow.Core;
using EventFlow.Core.Caching;
using EventFlow.Core.RetryStrategies;
using EventFlow.Exceptions;
using EventFlow.Extensions;
using EventFlow.MongoDB.ReadStores;
using EventFlow.MongoDB.ValueObjects;
using EventFlow.ReadStores;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace MyTelegram.EventFlow.MongoDB;

public interface IMongoDbContext
{
    IMongoDatabase GetDatabase();
}

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _mongoDatabase;

    public MongoDbContext(IMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
    }

    public IMongoDatabase GetDatabase() => _mongoDatabase;
}

public class DefaultReadModelMongoDbContext : IMongoDbContext
{
    private readonly IConfiguration _configuration;
    private IMongoDatabase? _database;

    public DefaultReadModelMongoDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IMongoDatabase GetDatabase()
    {
        if (_database == null)
        {
            var connectionString = _configuration.GetConnectionString(GetConnectionStringName());
            var databaseName = _configuration.GetValue<string>(GetKeyOfDatabaseNameInConfiguration());
            var client = new MongoClient(connectionString);
            //client.Settings.MaxConnecting = 5000;
            //client.Settings.MaxConnectionPoolSize = 5000;
            _database = client.GetDatabase(databaseName);
        }

        return _database;
    }

    protected virtual string GetConnectionStringName() => "Default";
    protected virtual string GetKeyOfDatabaseNameInConfiguration() => "App:DatabaseName";
}

public interface IMyMongoDbReadModelStore<TReadModel> : IMongoDbReadModelStore<TReadModel> where TReadModel : class, IReadModel
{
    Task<IAsyncCursor<TResult>> FindAsync<TResult>(
        Expression<Func<TReadModel, bool>> filter,
        FindOptions<TReadModel, TResult>? options = null,
        CancellationToken cancellationToken = default);

    Task<long> CountAsync(Expression<Func<TReadModel, bool>>? filter = null, CancellationToken cancellationToken = default);

    Task<IAggregateFluent<TResult>> AggregateAsync<TResult, TKey>(
        Expression<Func<TReadModel, bool>> filter,
        Expression<Func<TReadModel, TKey>> id,
        Expression<Func<IGrouping<TKey, TReadModel>, TResult>> group,
        AggregateOptions? options = null,
        CancellationToken cancellationToken = default);
}

public class MyMongoDbReadModelStore<TReadModel> :
    MyMongoDbReadModelStore<TReadModel, IMongoDbContext>
    where TReadModel : class, IMongoDbReadModel
{
    public MyMongoDbReadModelStore(ILogger<MongoDbReadModelStore<TReadModel>> logger, IReadModelDescriptionProvider readModelDescriptionProvider, ITransientFaultHandler<IOptimisticConcurrencyRetryStrategy> transientFaultHandler, IMongoDbContextFactory<IMongoDbContext> dbContextFactory, IMemoryCache memoryCache, IReadModelCacheStrategy readModelCacheStrategy, IReadModelUpdateStrategy readModelUpdateStrategy, IReadModelUpdateManager readModelUpdateManager) : base(logger, readModelDescriptionProvider, transientFaultHandler, dbContextFactory, memoryCache, readModelCacheStrategy, readModelUpdateStrategy, readModelUpdateManager)
    {
    }
}

public interface IReadModelCacheStrategy
{
    Task<bool> ShouldCacheReadModelAsync<TReadModel>();
}

public class DefaultReadModelCacheStrategy : IReadModelCacheStrategy
{
    public Task<bool> ShouldCacheReadModelAsync<TReadModel>()
    {
        return Task.FromResult(false);
    }
}

public interface IReadModelUpdateStrategy
{
    Task<bool> ShouldUpdateReadModelAsync<TReadModel>();
}

public class ReadModelUpdateStrategy : IReadModelUpdateStrategy
{
    public Task<bool> ShouldUpdateReadModelAsync<TReadModel>()
    {
        return Task.FromResult(true);
    }
}

public interface IReadModelUpdateManager
{
    Task<UpdateStrategy> GetReadModelUpdateStrategyAsync<TReadModel>();
}

public class ReadModelUpdateManager : IReadModelUpdateManager
{
    public Task<UpdateStrategy> GetReadModelUpdateStrategyAsync<TReadModel>()
    {
        return Task.FromResult(UpdateStrategy.UpdateDatabase);
    }
}

public enum UpdateStrategy
{
    None,
    All,
    UpdateDatabase,
    UpdateCache,
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

public class MongoDbReadModelStore<TReadModel> : IMongoDbReadModelStore<TReadModel>
       where TReadModel : class, IMongoDbReadModel
{
    private readonly ILogger<MongoDbReadModelStore<TReadModel>> _logger;
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IReadModelDescriptionProvider _readModelDescriptionProvider;
    private readonly ITransientFaultHandler<IOptimisticConcurrencyRetryStrategy> _transientFaultHandler;

    public MongoDbReadModelStore(
        ILogger<MongoDbReadModelStore<TReadModel>> logger,
        IMongoDatabase mongoDatabase,
        IReadModelDescriptionProvider readModelDescriptionProvider,
        ITransientFaultHandler<IOptimisticConcurrencyRetryStrategy> transientFaultHandler)
    {
        _logger = logger;
        _mongoDatabase = mongoDatabase;
        _readModelDescriptionProvider = readModelDescriptionProvider;
        _transientFaultHandler = transientFaultHandler;
    }

    public virtual async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();

        _logger.LogInformation(
            "Deleting '{ReadModelType}' with id '{Id}', from '{@RootCollectionName}'!",
            typeof(TReadModel).PrettyPrint(),
            id,
            readModelDescription.RootCollectionName);

        var collection = _mongoDatabase.GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);
        await collection.DeleteOneAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task DeleteAllAsync(CancellationToken cancellationToken)
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();

        _logger.LogInformation(
            "Deleting ALL '{ReadModelType}' by DROPPING COLLECTION '{@RootCollectionName}'!",
            typeof(TReadModel).PrettyPrint(),
            readModelDescription.RootCollectionName);

        await _mongoDatabase.DropCollectionAsync(readModelDescription.RootCollectionName.Value, cancellationToken);
    }

    public virtual async Task<ReadModelEnvelope<TReadModel>> GetAsync(string id, CancellationToken cancellationToken)
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();

        _logger.LogTrace(
            "Fetching read model '{ReadModelType}' with _id '{Id}' from collection '{@RootCollectionName}'",
            typeof(TReadModel).PrettyPrint(),
            id,
            readModelDescription.RootCollectionName);

        var collection = _mongoDatabase.GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);
        var filter = Builders<TReadModel>.Filter.Eq(readModel => readModel.Id, id);
        var result = await collection.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (result == null)
        {
            return ReadModelEnvelope<TReadModel>.Empty(id);
        }

        return ReadModelEnvelope<TReadModel>.With(id, result);
    }

    public async Task<IAsyncCursor<TReadModel>> FindAsync(Expression<Func<TReadModel, bool>> filter,
        FindOptions<TReadModel, TReadModel> options = null,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = _mongoDatabase.GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);

        _logger.LogTrace(
            "Finding read model '{ReadModelType}' with expression '{Filter}' from collection '{RootCollectionName}'",
            typeof(TReadModel).PrettyPrint(),
            filter.ToString(),
            readModelDescription.RootCollectionName.ToString());

        return await collection.FindAsync(filter, options, cancellationToken);
    }

    protected virtual async Task UpdateReadModelAsync(ReadModelDescription readModelDescription,
        ReadModelUpdate readModelUpdate,
        IReadModelContextFactory readModelContextFactory,
        Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken,
            Task<ReadModelUpdateResult<TReadModel>>> updateReadModel,
        CancellationToken cancellationToken)
    {
        var collection = _mongoDatabase.GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);
        var filter = Builders<TReadModel>.Filter.Eq(readmodel => readmodel.Id, readModelUpdate.ReadModelId);
        var result = collection.Find(filter).FirstOrDefault();

        var isNew = result == null;

        var readModelEnvelope = !isNew
            ? ReadModelEnvelope<TReadModel>.With(readModelUpdate.ReadModelId, result)
            : ReadModelEnvelope<TReadModel>.Empty(readModelUpdate.ReadModelId);

        var readModelContext = readModelContextFactory.Create(readModelUpdate.ReadModelId, isNew);
        var readModelUpdateResult =
            await updateReadModel(readModelContext, readModelUpdate.DomainEvents, readModelEnvelope,
                cancellationToken).ConfigureAwait(false);

        if (!readModelUpdateResult.IsModified)
        {
            return;
        }

        if (readModelContext.IsMarkedForDeletion)
        {
            await DeleteAsync(readModelUpdate.ReadModelId, cancellationToken);
            return;
        }

        readModelEnvelope = readModelUpdateResult.Envelope;
        var originalVersion = readModelEnvelope.ReadModel.Version;
        readModelEnvelope.ReadModel.Version = readModelEnvelope.Version;
        try
        {
            await collection.ReplaceOneAsync(
                x => x.Id == readModelUpdate.ReadModelId && x.Version == originalVersion,
                readModelEnvelope.ReadModel,
                new ReplaceOptions { IsUpsert = true },
                cancellationToken);
        }
        catch (MongoWriteException e)
        {
            throw new OptimisticConcurrencyException(
                $"Read model '{readModelUpdate.ReadModelId}' updated by another",
                e);
        }
    }

    public virtual async Task UpdateAsync(IReadOnlyCollection<ReadModelUpdate> readModelUpdates,
        IReadModelContextFactory readModelContextFactory,
        Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken,
            Task<ReadModelUpdateResult<TReadModel>>> updateReadModel,
        CancellationToken cancellationToken)
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();

        foreach (var readModelUpdate in readModelUpdates)
        {
            await _transientFaultHandler.TryAsync(
                    c => UpdateReadModelAsync(readModelDescription, readModelUpdate, readModelContextFactory,
                        updateReadModel, c),
                    Label.Named("mongodb-read-model-update"),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }

    public IQueryable<TReadModel> AsQueryable()
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = _mongoDatabase.GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);
        return collection.AsQueryable();
    }
}