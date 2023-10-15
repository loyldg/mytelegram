using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Core;
using EventFlow.Core.Caching;
using EventFlow.Core.RetryStrategies;
using EventFlow.Exceptions;
using EventFlow.Extensions;
using EventFlow.MongoDB.ReadStores;
using EventFlow.MongoDB.ValueObjects;
using EventFlow.ReadStores;
using EventFlow.ReadStores.InMemory;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

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



//public interface IMongoDbReadModelDatabaseFactory
//{
//    IMongoDatabase CreateMongoDatabase<TReadModel>();
//    void TryAddMongoDatabase<TReadModel>(IMongoDatabase database);
//}

//public class MongoDbReadModelDatabaseFactory : IMongoDbReadModelDatabaseFactory
//{
//    private readonly ConcurrentDictionary<Type, IMongoDatabase> _readModelTypeToDatabases = new();

//    public IMongoDatabase CreateMongoDatabase<TReadModel>()
//    {
//        if (_readModelTypeToDatabases.TryGetValue(typeof(TReadModel), out var db))
//        {
//            return db;
//        }

//        throw new Exception($"Not configure mongodb database for readModel '{typeof(TReadModel)}'");
//    }

//    public void TryAddMongoDatabase<TReadModel>(IMongoDatabase database)
//    {
//        _readModelTypeToDatabases.TryAdd(typeof(TReadModel), database);
//    }
//}


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
        //if (!await _readModelUpdateStrategy.ShouldUpdateReadModelAsync<TReadModel>())
        //{
        //    return;
        //}

        var updateStrategy = await _readModelUpdateManager.GetReadModelUpdateStrategyAsync<TReadModel>();

        await base.UpdateAsync(readModelUpdates, readModelContextFactory, updateReadModel, cancellationToken);
        switch (updateStrategy)
        {
            case UpdateStrategy.None:
                return;
            case UpdateStrategy.All:
                Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken,
                    Task<ReadModelUpdateResult<TReadModel>>> updateReadModelWithCaching = (context, events, envelope, token) =>
                    updateReadModel(context, events, envelope, token).ContinueWith(result =>
                    {
                        if (!result.IsFaulted)
                        {
                            _memoryCache.Set(CacheKey.With(result.Result.Envelope.ReadModelId), result.Result.Envelope);
                        }

                        return result.Result;
                    }, token);

                await base.UpdateAsync(readModelUpdates, readModelContextFactory, updateReadModelWithCaching, cancellationToken);
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
                    var aggregateEvent= readModelUpdate.DomainEvents.ElementAt(0).GetAggregateEvent();
                    _memoryCache.Set(CacheKey.With(typeof(TReadModel), readModelId), readModelEnvelope);
                    Console.WriteLine($"Update in memory readmodel:{readModelId},{aggregateEvent.GetType().Name}");
                }
            }
        }
    }

    //protected virtual TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromDays(3);
}

//public class MyInMemoryCacheReadModelStore<TReadModel>
//where TReadModel : class, IMongoDbReadModel
//{
//    public Task UpdateAsync(IReadOnlyCollection<ReadModelUpdate> readModelUpdates,
//          IReadModelContextFactory readModelContextFactory,
//          Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken,
//              Task<ReadModelUpdateResult<TReadModel>>> updateReadModel, CancellationToken cancellationToken)
//    {

//    }
//}

//public interface IMyInMemoryReadStore<TReadModel> : IInMemoryReadStore<TReadModel> where TReadModel : class, IReadModel
//{
//    Task<IQueryable<TReadModel>> AsQueryable(CancellationToken cancellationToken = default);

//    void Add(string id, TReadModel readModel, long? version);
//}
//public class MyInMemoryReadStore<TReadModel> : ReadModelStore<TReadModel>, IMyInMemoryReadStore<TReadModel>
//        where TReadModel : class, IReadModel
//{
//    private readonly ConcurrentDictionary<string, ReadModelEnvelope<TReadModel>> _readModels = new();

//    public MyInMemoryReadStore(ILogger<MyInMemoryReadStore<TReadModel>> logger) : base(logger)
//    {
//    }

//    public override Task<ReadModelEnvelope<TReadModel>> GetAsync(string id, CancellationToken cancellationToken)
//    {
//        if (_readModels.TryGetValue(id, out var readModelEnvelope))
//        {
//            return Task.FromResult(readModelEnvelope);
//        }

//        return Task.FromResult(ReadModelEnvelope<TReadModel>.Empty(id));
//    }

//    public override async Task UpdateAsync(IReadOnlyCollection<ReadModelUpdate> readModelUpdates, IReadModelContextFactory readModelContextFactory,
//        Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken, Task<ReadModelUpdateResult<TReadModel>>> updateReadModel, CancellationToken cancellationToken)
//    {
//        foreach (var readModelUpdate in readModelUpdates)
//        {
//            var readModelId = readModelUpdate.ReadModelId;

//            //var isNew = !_readModels.TryGetValue(readModelId, out var readModelEnvelope);
//            var isNew = !_readModels.TryGetValue(readModelId, out var readModelEnvelope);

//            if (isNew)
//            {
//                continue;
//            }

//            if (isNew)
//            {
//                readModelEnvelope = ReadModelEnvelope<TReadModel>.Empty(readModelId);
//            }

//            Console.WriteLine($"isNew:{isNew} version:{readModelEnvelope.Version}");
//            var readModelContext = readModelContextFactory.Create(readModelId, isNew);

//            var readModelUpdateResult = await updateReadModel(
//                    readModelContext,
//                    readModelUpdate.DomainEvents,
//                    readModelEnvelope,
//                    cancellationToken)
//                .ConfigureAwait(false);
//            if (!readModelUpdateResult.IsModified)
//            {
//                continue;
//            }
//            var oldReadModelEnvelope = readModelEnvelope;
//            var oldVersion = readModelEnvelope.Version;
//            readModelEnvelope = readModelUpdateResult.Envelope;
//            var newVersion = readModelEnvelope.Version;
//            Console.WriteLine($"After update version={readModelUpdateResult.Envelope.Version} oldVersion:{oldVersion} newVersion:{newVersion}");

//            if (readModelContext.IsMarkedForDeletion)
//            {
//                _readModels.TryRemove(readModelId, out _);
//            }
//            else
//            {
//                //_readModels.AddOrUpdate(readModelId, readModelEnvelope, (rid, oldData) => readModelEnvelope);
//                //_readModels.TryUpdate(readModelId, readModelEnvelope, null);
//                //if (_readModels.ContainsKey(readModelId))
//                //{
//                //    _readModels.TryRemove(readModelId,out _);
//                //}

//                //_readModels.TryAdd(readModelId, readModelEnvelope);
//                if (isNew)
//                {
//                    _readModels.TryAdd(readModelId, readModelEnvelope);
//                }
//                else
//                {
//                    if (!_readModels.TryUpdate(readModelId, readModelEnvelope, oldReadModelEnvelope))
//                    {
//                        Console.WriteLine("Update inmemory readmodel failed");
//                    }
//                }

//                Console.WriteLine($"Update in-memory readmodel({typeof(TReadModel)}) {readModelUpdates.Count} ({readModelUpdate.DomainEvents.FirstOrDefault()?.GetAggregateEvent().GetType().Name}):{readModelEnvelope.ReadModelId} total:{_readModels.Count} version:{oldVersion}->{readModelEnvelope.Version}");
//            }
//        }
//    }

//    public async Task<IReadOnlyCollection<TReadModel>> FindAsync(Predicate<TReadModel> predicate, CancellationToken cancellationToken)
//    {
//        return _readModels.Values
//            .Where(p => predicate(p.ReadModel))
//            .Select(p => p.ReadModel)
//            .ToList();
//    }

//    public Task<IQueryable<TReadModel>> AsQueryable(CancellationToken cancellationToken = default)
//    {
//        return Task.FromResult(_readModels.Values.Select(p => p.ReadModel).AsQueryable());
//    }

//    public void Add(string id, TReadModel readModel, long? version)
//    {
//        _readModels.TryAdd(id, ReadModelEnvelope<TReadModel>.With(id, readModel, version));
//    }

//    public override Task DeleteAsync(string id, CancellationToken cancellationToken)
//    {
//        _readModels.TryRemove(id, out _);

//        return Task.CompletedTask;
//    }

//    public override Task DeleteAllAsync(CancellationToken cancellationToken)
//    {
//        _readModels.Clear();

//        return Task.CompletedTask;
//    }
//}


public abstract class CachedReadModelStore<TReadModel> : IReadModelStore<TReadModel>
        where TReadModel : class, IReadModel
{
    private readonly IMemoryCache _memoryCache;

    protected ILogger Logger { get; }

    protected CachedReadModelStore(
        ILogger logger,
        IMemoryCache memoryCache)
    {
        Logger = logger;
        this._memoryCache = memoryCache;
    }

    public async Task<ReadModelEnvelope<TReadModel>> GetAsync(string id, CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync(CacheKey.With(this.GetType(), id), entry =>
        {
            entry.SetSlidingExpiration(TimeSpan.FromMinutes(30)); //Make configurable
            return GetEntryAsync(id, cancellationToken);
        });
    }
    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        _memoryCache.Remove(CacheKey.With(this.GetType(), id));

        await DeleteEntryAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(IReadOnlyCollection<ReadModelUpdate> readModelUpdates,
        IReadModelContextFactory readModelContextFactory,
        Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken,
            Task<ReadModelUpdateResult<TReadModel>>> updateReadModel,
        CancellationToken cancellationToken)
    {
        Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken,
            Task<ReadModelUpdateResult<TReadModel>>> updateReadModelWithCaching = (context, events, envelope, token) => updateReadModel(context, events, envelope, token)
            .ContinueWith(result =>
            {
                var cacheNonFaulted = !result.IsFaulted;
                if (cacheNonFaulted)
                {
                    _memoryCache.Set(CacheKey.With(result.Result.Envelope.ReadModelId), result.Result.Envelope);
                }
                return result.Result;
            });

        await UpdateEntryAsync(readModelUpdates, readModelContextFactory, updateReadModelWithCaching, cancellationToken);
    }

    public abstract Task<ReadModelEnvelope<TReadModel>> GetEntryAsync(
        string id,
        CancellationToken cancellationToken);

    public abstract Task DeleteEntryAsync(
        string id,
        CancellationToken cancellationToken);

    public abstract Task DeleteAllAsync(
        CancellationToken cancellationToken);

    public abstract Task UpdateEntryAsync(IReadOnlyCollection<ReadModelUpdate> readModelUpdates,
        IReadModelContextFactory readModelContextFactory,
        Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken,
            Task<ReadModelUpdateResult<TReadModel>>> updateReadModel,
        CancellationToken cancellationToken);
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