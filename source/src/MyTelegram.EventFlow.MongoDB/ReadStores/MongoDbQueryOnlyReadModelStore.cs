using EventFlow.Extensions;
using EventFlow.ReadStores;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MyTelegram.EventFlow.ReadStores;
using System.Linq.Expressions;

namespace MyTelegram.EventFlow.MongoDB.ReadStores;

public class MongoDbQueryOnlyReadModelStore<TReadModel>(
    IQueryOnlyReadModelDescriptionProvider readModelDescriptionProvider,
    IMongoDbContext dbContext,
    ILogger<MongoDbQueryOnlyReadModelStore<TReadModel, IMongoDbContext>> logger)
    : MongoDbQueryOnlyReadModelStore<TReadModel, IMongoDbContext>(readModelDescriptionProvider, dbContext,
        logger)
    where TReadModel : class, IReadModel;

public class MongoDbQueryOnlyReadModelStore<TReadModel, TDbContext>(
        IQueryOnlyReadModelDescriptionProvider readModelDescriptionProvider,
        TDbContext dbContext,
        ILogger<MongoDbQueryOnlyReadModelStore<TReadModel, TDbContext>> logger)
    : IQueryOnlyReadModelStore<TReadModel>
    where TReadModel : class, IReadModel
    where TDbContext : IMongoDbContext
{
    public IQueryable<TReadModel> GetAll()
    {
        var readModelDescription = readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = GetDatabase().GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);

        return collection.AsQueryable();
    }

    public async Task<List<TResult>> GroupByAsync<TKey, TResult>(
        Expression<Func<TReadModel, bool>>? filter,
        Expression<Func<TReadModel, TKey>> keySelector,
        Expression<Func<IGrouping<TKey, TReadModel>, TResult>> resultSelector)
    {
        var readModelDescription = readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = GetDatabase().GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);
        var query = collection.Aggregate();

        if (filter != null)
        {
            query = query.Match(filter);
        }

        return await query
            .Group(keySelector, resultSelector)
            .ToListAsync();
    }

    public Task<IReadOnlyCollection<TReadModel>> FindAsync(Expression<Func<TReadModel, bool>> filter, int skip = 0,
        int limit = 0,
        SortOptions<TReadModel>? sort = null,
        CancellationToken cancellationToken = default)
    {
        return FindAsync(filter, p => p, skip, limit, sort, cancellationToken);
    }

    public async Task<IReadOnlyCollection<TResult>> FindAsync<TResult>(Expression<Func<TReadModel, bool>> filter,
        Expression<Func<TReadModel, TResult>> createResult, int skip = 0,
        int limit = 0, SortOptions<TReadModel>? sort = null, CancellationToken cancellationToken = default)
    {
        var findOptions = CreateFindOptions(createResult, skip, limit, sort);
        var cursor = await FindCoreAsync(filter, findOptions, cancellationToken);

        return await cursor.ToListAsync(cancellationToken);
    }

    public async Task<TReadModel?> FirstOrDefaultAsync(Expression<Func<TReadModel, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        var cursor = await FindCoreAsync<TReadModel>(filter, cancellationToken: cancellationToken);
        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<TReadModel, bool>> filter,
        Expression<Func<TReadModel, TResult>> createResult,
        SortOptions<TReadModel>? sort = null,
        CancellationToken cancellationToken = default)
    {
        var findOptions = CreateFindOptions(createResult, 0, 0, sort);

        var cursor = await FindCoreAsync(filter, findOptions, cancellationToken);
        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }

    public Task<long> CountAsync(Expression<Func<TReadModel, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        var readModelDescription = readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = GetDatabase().GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);

        return collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
    }

    private static FindOptions<T1, T2> CreateFindOptions<T1, T2>(
        Expression<Func<T1, T2>> createResult,
        int skip = 0,
        int limit = 0, SortOptions<T1>? sort = null)
    {
        var findOptions = new FindOptions<T1, T2>
        {
            Projection = Builders<T1>.Projection.Expression(createResult)
        };
        if (skip > 0)
        {
            findOptions.Skip = skip;
        }

        if (limit > 0)
        {
            findOptions.Limit = limit;
        }

        if (sort != null)
        {
            findOptions.Sort = sort.SortType switch
            {
                SortType.None or SortType.Ascending => Builders<T1>.Sort.Ascending(sort.Sort),
                SortType.Descending => Builders<T1>.Sort.Descending(sort.Sort),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        return findOptions;
    }

    private async Task<IAsyncCursor<TResult>> FindCoreAsync<TResult>(Expression<Func<TReadModel, bool>> filter,
        FindOptions<TReadModel, TResult>? options = null, CancellationToken cancellationToken = default)
    {
        var readModelDescription = readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = GetDatabase().GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);

        logger.LogTrace(
            "Finding read model '{ReadModel}' with expression '{Filter}' from collection '{RootCollectionName}'",
            typeof(TReadModel).PrettyPrint(),
            filter,
            readModelDescription.RootCollectionName);

        return await collection.FindAsync(filter, options, cancellationToken);
    }

    private IMongoDatabase GetDatabase()
    {
        return dbContext.GetDatabase();
    }
}