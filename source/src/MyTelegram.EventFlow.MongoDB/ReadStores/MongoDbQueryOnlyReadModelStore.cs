using System.Diagnostics.CodeAnalysis;
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

        var query = collection.AsQueryable();

        if (!QueryFilterScope.Current.IgnoreSoftDelete &&
            typeof(ISoftDelete).IsAssignableFrom(typeof(TReadModel)))
        {
            query = query.Where(x => !((ISoftDelete)x).IsDeleted);
        }

        return query;
    }

    public async Task<List<TResult>> GroupByAsync<TKey, TResult>(
        Expression<Func<TReadModel, bool>>? filter,
        Expression<Func<TReadModel, TKey>> keySelector,
        Expression<Func<IGrouping<TKey, TReadModel>, TResult>> resultSelector)
    {
        var readModelDescription = readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = GetDatabase().GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);
        var query = collection.Aggregate();
        var finalFilter = ApplySoftDeleteFilter(filter);
        if (finalFilter != null!)
        {
            query = query.Match(finalFilter);
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
        var finalFilter = ApplySoftDeleteFilter(filter);

        return FindAsync(finalFilter, p => p, skip, limit, sort, cancellationToken);
    }

    public async Task<IReadOnlyCollection<TResult>> FindAsync<TResult>(Expression<Func<TReadModel, bool>> filter,
        Expression<Func<TReadModel, TResult>> createResult, int skip = 0,
        int limit = 0, SortOptions<TReadModel>? sort = null, CancellationToken cancellationToken = default)
    {
        var finalFilter = ApplySoftDeleteFilter(filter);

        var findOptions = CreateFindOptions(createResult, skip, limit, sort);
        var cursor = await FindCoreAsync(finalFilter, findOptions, cancellationToken);

        return await cursor.ToListAsync(cancellationToken);
    }

    public async Task<TReadModel?> FirstOrDefaultAsync(Expression<Func<TReadModel, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        var finalFilter = ApplySoftDeleteFilter(filter);

        var cursor = await FindCoreAsync<TReadModel>(finalFilter, cancellationToken: cancellationToken);
        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<TReadModel, bool>> filter,
        Expression<Func<TReadModel, TResult>> createResult,
        SortOptions<TReadModel>? sort = null,
        CancellationToken cancellationToken = default)
    {
        var findOptions = CreateFindOptions(createResult, 0, 0, sort);
        var finalFilter = ApplySoftDeleteFilter(filter);

        var cursor = await FindCoreAsync(finalFilter, findOptions, cancellationToken);
        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }

    public Task<long> CountAsync(Expression<Func<TReadModel, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        var readModelDescription = readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = GetDatabase().GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);

        var finalFilter = ApplySoftDeleteFilter(filter);

        return collection.CountDocumentsAsync(finalFilter, cancellationToken: cancellationToken);
    }

    [return: NotNullIfNotNull(nameof(filter))]
    private Expression<Func<TReadModel, bool>>? ApplySoftDeleteFilter(
        Expression<Func<TReadModel, bool>>? filter)
    {
        if (QueryFilterScope.Current.IgnoreSoftDelete || !typeof(ISoftDelete).IsAssignableFrom(typeof(TReadModel)))
        {
            return filter;
        }

        Expression<Func<TReadModel, bool>> notDeleted =
            x => !((ISoftDelete)x).IsDeleted;

        return filter == null
            ? notDeleted
            : filter.And(notDeleted);
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
        var finalFilter = ApplySoftDeleteFilter(filter);
        var readModelDescription = readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = GetDatabase().GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);

        logger.LogTrace(
            "Finding read model '{ReadModel}' with expression '{Filter}' from collection '{RootCollectionName}'",
            typeof(TReadModel).PrettyPrint(),
            finalFilter,
            readModelDescription.RootCollectionName);

        return await collection.FindAsync(finalFilter, options, cancellationToken);
    }

    private IMongoDatabase GetDatabase()
    {
        return dbContext.GetDatabase();
    }
}