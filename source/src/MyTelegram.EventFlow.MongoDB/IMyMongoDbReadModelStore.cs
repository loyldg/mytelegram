using System.Linq.Expressions;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using MongoDB.Driver;

namespace MyTelegram.EventFlow.MongoDB;

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